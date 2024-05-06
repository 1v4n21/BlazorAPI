using BlazorCrud.Share;

namespace BlazorCrrud.Client.Services
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoDTO>> Lista();
    }
}
