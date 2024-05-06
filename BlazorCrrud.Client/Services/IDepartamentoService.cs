using BlazorCrud.Share;

namespace BlazorCrud.Client.Services
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoDTO>> Lista();
    }
}
