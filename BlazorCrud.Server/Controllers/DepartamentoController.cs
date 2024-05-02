using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Share;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartamentoController : ControllerBase
	{
		private readonly DbcrudBlazorContext _dbContext;

        public DepartamentoController(DbcrudBlazorContext dbContext)
        {
			_dbContext = dbContext;
        }

		[HttpGet]
		[Route("Lista")]
		public async Task<IActionResult> Lista()
		{
			var responseAPI = new ResponseAPI<List<DepartamentoDTO>>();
			var listaDepartamentoDTO = new List<DepartamentoDTO>();

			try
			{
				foreach(var item in await _dbContext.Departamentos.ToListAsync()) 
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
