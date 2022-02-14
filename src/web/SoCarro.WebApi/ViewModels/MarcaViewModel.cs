using SoCarro.Core.WebApi;
using System.ComponentModel.DataAnnotations;

namespace SoCarro.WebApi.ViewModels;

public class MarcaViewModel : EntityViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get;  set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Status { get;  set; }
    public VeiculoViewModel Veiculo { get;  set; }
}
