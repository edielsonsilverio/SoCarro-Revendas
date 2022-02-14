using SoCarro.Core.Comunication;
using SoCarro.WebApp.MVC.ViewModels;

namespace SoCarro.WebApp.MVC.Services;

public interface IVeiculoService
{
    Task<IEnumerable<VeiculoViewModel>> ObterTodos();
    Task<VeiculoViewModel> ObterPorId(Guid id);
    Task<ModeloViewModel> ObterModeloPorId(Guid id);
    Task<ResponseResult> Adicionar(VeiculoViewModel model);
    Task<ResponseResult> Atualizar(VeiculoViewModel model);
    Task<ResponseResult> MudarStatus(VeiculoViewModel model);
}
