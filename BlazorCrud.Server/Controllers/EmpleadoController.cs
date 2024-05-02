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

		public EmpleadoController(DbcrudBlazorContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		[Route("Lista")]
		public async Task<IActionResult> Lista()
		{
			var responseAPI = new ResponseAPI<List<EmpleadoDTO>>();
			var listaEmpleadoDTO = new List<EmpleadoDTO>();

			try
			{
				foreach (var item in await _dbContext.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
				{
					listaEmpleadoDTO.Add(new EmpleadoDTO
					{
						IdEmpleado = item.IdEmpleado,
						NombreCompleto = item.NombreCompleto,
						IdDepartamento = item.IdDepartamento,
						Sueldo = item.Sueldo,
						FechaContrato = item.FechaContrato,
						Departamento = new DepartamentoDTO
						{
							IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
							Nombre = item.IdDepartamentoNavigation.Nombre,
						}
					});

				}

				responseAPI.EsCorrecto = true;
				responseAPI.Valor = listaEmpleadoDTO;
			}
			catch (Exception ex)
			{
				responseAPI.EsCorrecto = false;
				responseAPI.Mensaje = ex.Message;
			}

			return Ok(responseAPI);
		}

		[HttpGet]
		[Route("Buscar/{id}")]
		public async Task<IActionResult> Buscar(int id)
		{
			var responseAPI = new ResponseAPI<List<DepartamentoDTO>>();
			var listaDepartamentoDTO = new List<DepartamentoDTO>();

			try
			{
				foreach (var item in await _dbContext.Departamentos.ToListAsync())
				{
					listaDepartamentoDTO.Add(new DepartamentoDTO
					{
						IdDepartamento = item.IdDepartamento,
						Nombre = item.Nombre,
					});

				}

				responseAPI.EsCorrecto = true;
				responseAPI.Valor = listaDepartamentoDTO;
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
