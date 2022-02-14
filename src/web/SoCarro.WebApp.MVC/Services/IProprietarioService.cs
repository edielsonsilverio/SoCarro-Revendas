using SoCarro.Core.Comunication;
using SoCarro.WebApp.MVC.ViewModels;

namespace SoCarro.WebApp.MVC.Services;

public interface IProprietarioService
{
    Task<IEnumerable<ProprietarioViewModel>> ObterTodos();
    Task<ProprietarioViewModel> ObterPorId(Guid id);
    Task<ResponseResult> Adicionar(ProprietarioViewModel model);
    Task<ResponseResult> Atualizar(ProprietarioViewModel model);
    Task<ResponseResult> MudarStatus(ProprietarioViewModel model);
}
