using SoCarro.Core.Comunication;
using SoCarro.WebApp.MVC.ViewModels;

namespace SoCarro.WebApp.MVC.Services;

public interface IMarcaService
{
    Task<IEnumerable<MarcaViewModel>> ObterTodos();
    Task<MarcaViewModel> ObterPorId(Guid id);
    Task<ResponseResult> Adicionar(MarcaViewModel model);
    Task<ResponseResult> Atualizar(MarcaViewModel model);
    Task<ResponseResult> MudarStatus(MarcaViewModel model);
}
