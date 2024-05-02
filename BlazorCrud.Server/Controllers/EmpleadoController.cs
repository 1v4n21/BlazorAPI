using BlazorCrud.Server.Models;
using BlazorCrud.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmpleadoController : ControllerBase
	{
		private readonly DbcrudBlazorContext _dbContext;

		//Constructor para obtener la base de datos utilizada con su cadena de conexion
		public EmpleadoController(DbcrudBlazorContext dbContext)
		{
			//Obtenemos la base de datos
			_dbContext = dbContext;
		}

		//Acepta peticiones get http y la ruta es api/controlador/Lista para acceder
		//Obtener una lisa de todos los empleados
		[HttpGet]
		[Route("Lista")]
		public async Task<IActionResult> Lista()
		{
			//Respuesta al acceder a la ruta
			var responseAPI = new ResponseAPI<List<EmpleadoDTO>>();

			//Lista de empleados que se va a obtener
			var listaEmpleadoDTO = new List<EmpleadoDTO>();

			try
			{
				//Vamos guardando los empleados de uno en uno en la lista empleados
				foreach (var item in await _dbContext.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
				{
					listaEmpleadoDTO.Add(new EmpleadoDTO
					{
						//Insertamos todos los atributos del objeto empleado
						IdEmpleado = item.IdEmpleado,
						NombreCompleto = item.NombreCompleto,
						IdDepartamento = item.IdDepartamento,
						Sueldo = item.Sueldo,
						FechaContrato = item.FechaContrato,
						Departamento = new DepartamentoDTO
						{
							//Obtenemos el departamento del empleado
							IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
							Nombre = item.IdDepartamentoNavigation.Nombre,
						}
					});

				}

				//Si no da error es igual a true
				responseAPI.EsCorrecto = true;

				//Guardamos el valor de respuesta de la API
				responseAPI.Valor = listaEmpleadoDTO;
			}
			catch (Exception ex)
			{
				//Si da error es igual a false
				responseAPI.EsCorrecto = false;

				//Guardamos el mensaje de error de la excepcion
				responseAPI.Mensaje = ex.Message;
			}

			//Returnamos la respuesta de la API con un OK
			return Ok(responseAPI);
		}

		//Acepta peticiones get http y la ruta es api/controlador/Buscar/{id} para acceder
		//Obtener un empleado en concreto
		[HttpGet]
		[Route("Buscar/{id}")]
		public async Task<IActionResult> Buscar(int id)
		{
			var responseAPI = new ResponseAPI<EmpleadoDTO>();
			var empleadoDTO = new EmpleadoDTO();

			try {
				var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);
				
				if(dbEmpleado != null)
				{
					empleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
					empleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
					empleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
					empleadoDTO.Sueldo = dbEmpleado.Sueldo;
					empleadoDTO.FechaContrato = dbEmpleado.FechaContrato;

					responseAPI.EsCorrecto = true;
					responseAPI.Valor = empleadoDTO;
				}
				else
				{
					responseAPI.EsCorrecto = false;
					responseAPI.Mensaje = "No encontrado";
				}
			}
			catch (Exception ex)
			{
				responseAPI.EsCorrecto = false;
				responseAPI.Mensaje = ex.Message;
			}

			return Ok(responseAPI);
		}

		//Acepta peticiones post http y la ruta es api/controlador/Guardar
		//Guardar un empleado
		[HttpPost]
		[Route("Guardar")]
		public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
		{
			var responseAPI = new ResponseAPI<int>();

			try
			{
				var dbEmpleado = new Empleado
				{
					NombreCompleto = empleado.NombreCompleto,
					IdDepartamento = empleado.IdDepartamento,
					Sueldo = empleado.Sueldo,
					FechaContrato = empleado.FechaContrato,
				};

				_dbContext.Empleados.Add(dbEmpleado);
				await _dbContext.SaveChangesAsync();

				if (dbEmpleado.IdEmpleado != 0)
				{
					responseAPI.EsCorrecto = true;
					responseAPI.Valor = dbEmpleado.IdEmpleado;
				}
				else
				{
					responseAPI.EsCorrecto = false;
					responseAPI.Mensaje = "No guardado";
				}
			}
			catch (Exception ex)
			{
				responseAPI.EsCorrecto = false;
				responseAPI.Mensaje = ex.Message;
			}

			return Ok(responseAPI);
		}
	}
}
