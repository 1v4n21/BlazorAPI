using BlazorCrud.Share;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly HttpClient _http;

        public EmpleadoService(HttpClient http)
        {
            _http = http;
        }

        public Task<List<EmpleadoDTO>> Lista()
        {
            throw new NotImplementedException();
        }

        public Task<EmpleadoDTO> Buscar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Editar(EmpleadoDTO empleado)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Guardar(EmpleadoDTO empleado)
        {
            throw new NotImplementedException();
        }
    }
}
