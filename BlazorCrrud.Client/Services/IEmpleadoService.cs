using BlazorCrud.Share;

namespace BlazorCrrud.Client.Services
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTO>> Lista();
        Task<EmpleadoDTO> Buscar(int id);
        Task<int> Guardar(EmpleadoDTO empleado);
        Task<int> Editar(EmpleadoDTO empleado);
        Task<bool> Eliminar(int id);
    }
}
