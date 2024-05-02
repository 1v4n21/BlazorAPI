using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Share
{
	public class ResponseAPI<T>
	{
		//Atributos para la respuesta de nuestra API

		public bool EsCorrecto { get; set; }
		public T? Valor { get; set; }
        public string? Mensaje { get; set; }
    }
}
