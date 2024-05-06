using BlazorCrud.Share;
using System.Net.Http.Json;

namespace BlazorCrrud.Client.Services
{
    public class DepartamentoService : IDepartamentoService
    {

        private readonly HttpClient _http;

        //Constructor http para la ruta de la API
        public DepartamentoService(HttpClient http)
        {
            _http = http;
        }

        //Método para obtener la lista de departamentos llamando a la API
        public async Task<List<DepartamentoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<DepartamentoDTO>>>("api/Departamento/Lista");

            if(result!.EsCorrecto)
            {
                return result.Valor;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }
    }
}
