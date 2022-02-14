using System.ComponentModel.DataAnnotations;
using SoCarro.Core.WebApi;

namespace SoCarro.WebApp.MVC.ViewModels;

public class ModeloViewModel : EntityViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int? AnoFabricacao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int? AnoModelo { get; set; }

    public Guid VeiculoId { get; set; }
    public VeiculoViewModel Veiculo { get; set; }
}