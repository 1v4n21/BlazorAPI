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


        //Acepta peticiones get http y la ruta es api/Empleado/Lista para acceder
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


        //Acepta peticiones get http y la ruta es api/Empleado/Buscar/{id} para acceder
        //Obtener un empleado en concreto
        [HttpGet]
		[Route("Buscar/{id}")]
		public async Task<IActionResult> Buscar(int id)
		{
			//Respuesta de la API
			var responseAPI = new ResponseAPI<EmpleadoDTO>();

			//Empleado que buscamos
			var empleadoDTO = new EmpleadoDTO();

			try {
				//Realizamos la consulta por la id introducida
				var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);
				
				//Comprobamos que existe el empleado con esa id
				if(dbEmpleado != null)
				{
					//Obtenemos el empleado buscado por la id
					empleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
					empleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
					empleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
					empleadoDTO.Sueldo = dbEmpleado.Sueldo;
					empleadoDTO.FechaContrato = dbEmpleado.FechaContrato;

					//Returnamos que es correcto y el empleado
					responseAPI.EsCorrecto = true;
					responseAPI.Valor = empleadoDTO;
				}
				else
				{
					//En caso de que no exista un usuario con esa id
					responseAPI.EsCorrecto = false;
					responseAPI.Mensaje = "No encontrado";
				}
			}
			catch (Exception ex)
			{
				//En el caso de que returne una excepcion
				responseAPI.EsCorrecto = false;
				responseAPI.Mensaje = ex.Message;
			}

			//Returnamos la respuesta de la API y OK
			return Ok(responseAPI);
		}


        //Acepta peticiones post http y la ruta es api/Empleado/Guardar
        //Guardar un empleado
        [HttpPost]
		[Route("Guardar")]
		public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
		{
			//Respuesta de la API
			var responseAPI = new ResponseAPI<int>();

			try
			{
				//Creamos el nuevo empleado a guardar con sus atributos
				var dbEmpleado = new Empleado
				{
					NombreCompleto = empleado.NombreCompleto,
					IdDepartamento = empleado.IdDepartamento,
					Sueldo = empleado.Sueldo,
					FechaContrato = empleado.FechaContrato,
				};

				//Añadimos el nuevo empleado a la BD y guardamos cambios
				_dbContext.Empleados.Add(dbEmpleado);
				await _dbContext.SaveChangesAsync();

				//Comprobamos que se ha añadido correctamente el empleado
				if (dbEmpleado.IdEmpleado != 0)
				{
					//Returnamos que ha salido correctamente y la id del empleado
					responseAPI.EsCorrecto = true;
					responseAPI.Valor = dbEmpleado.IdEmpleado;
				}
				else
				{
					//Returnamos que ha dado error y no se ha guardado
					responseAPI.EsCorrecto = false;
					responseAPI.Mensaje = "No guardado";
				}
			}
			catch (Exception ex)
			{
				//Returnamos error en el caso de que se lance una excepcion
				responseAPI.EsCorrecto = false;
				responseAPI.Mensaje = ex.Message;
			}

			//Returnamos la respuesta de la API y OK
			return Ok(responseAPI);
		}

		//Acepta peticiones put http y la ruta es api/Empleado/Editar/{id}
		//Editar un empleado
		[HttpPut]
		[Route("Editar/{id}")]
		public async Task<IActionResult> Editar(EmpleadoDTO empleado, int id)
		{
			var responseAPI = new ResponseAPI<int>();

			try
			{
				var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

				if (dbEmpleado != null)
				{
					dbEmpleado.NombreCompleto = empleado.NombreCompleto;
					dbEmpleado.IdDepartamento = empleado.IdDepartamento;
					dbEmpleado.Sueldo = empleado.Sueldo;
					dbEmpleado.FechaContrato = empleado.FechaContrato;

					_dbContext.Empleados.Update(dbEmpleado);
					await _dbContext.SaveChangesAsync();

					responseAPI.EsCorrecto = true;
					responseAPI.Valor = dbEmpleado.IdEmpleado;
				}
				else
				{
					responseAPI.EsCorrecto = false;
					responseAPI.Mensaje = "Empleado no encontrado";
				}
			}
			catch (Exception ex)
			{
				responseAPI.EsCorrecto = false;
				responseAPI.Mensaje = ex.Message;
			}

			return Ok(responseAPI);
		}

		//Acepta peticiones delete http y la ruta es api/Empleado/Eliminar/{id}
		//Eliminar un empleado
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    _dbContext.Empleados.Remove(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Empleado no encontrado";
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
