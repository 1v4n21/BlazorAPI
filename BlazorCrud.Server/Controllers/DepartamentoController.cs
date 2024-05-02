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

		//Metodo para obtener la base de datos mediante la cadena de esta
        public DepartamentoController(DbcrudBlazorContext dbContext)
        {
			//Obtenemos la base de datos
			_dbContext = dbContext;
        }

		//Permite peticiones get por http a traves de la ruta /Lista
		//Obtener una lista de todos los departamentos
		[HttpGet]
		[Route("Lista")]
		public async Task<IActionResult> Lista()
		{
			//Respuesta de nuestra API
			var responseAPI = new ResponseAPI<List<DepartamentoDTO>>();

			//Lista de los departamentos
			var listaDepartamentoDTO = new List<DepartamentoDTO>();

			try
			{
				//Bucle para obtener todos los departamentos y guardarlos
				foreach(var item in await _dbContext.Departamentos.ToListAsync()) 
				{
					listaDepartamentoDTO.Add(new DepartamentoDTO
					{
						//Obtenemos todos los atributos del objeto departamento
						IdDepartamento = item.IdDepartamento,
						Nombre = item.Nombre,
					});

				}

				//Si no da error = true
				responseAPI.EsCorrecto = true;

				//Guardamos la lista de departamentos
				responseAPI.Valor = listaDepartamentoDTO;
			}
			catch (Exception ex)
			{
				//Si da error = false
				responseAPI.EsCorrecto = false;

				//Guardamos elmensaje de error
				responseAPI.Mensaje = ex.Message;
			}

			//Returnamos la respuesta de la API y Ok
			return Ok(responseAPI);
		}
    }
}
