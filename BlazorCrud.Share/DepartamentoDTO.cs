using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Share
{
	public class DepartamentoDTO
	{
		//Atributos de la clase departamento, Nombre no puede ser null

		public int IdDepartamento { get; set; }
		public string Nombre { get; set; } = null!;
	}
}
