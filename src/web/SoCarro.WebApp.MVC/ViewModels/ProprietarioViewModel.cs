using System.ComponentModel.DataAnnotations;
using SoCarro.Core.WebApi;

namespace SoCarro.WebApp.MVC.ViewModels;

public class ProprietarioViewModel : EntityViewModel
{

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
    public string Documento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Status { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int? TipoProprietario { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }
    public bool Ativo { get; set; } = true;
    public EnderecoViewModel Endereco { get; set; }
    public VeiculoViewModel Veiculo { get; set; }

}

