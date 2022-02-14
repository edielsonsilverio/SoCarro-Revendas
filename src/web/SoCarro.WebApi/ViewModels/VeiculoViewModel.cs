using SoCarro.Core.WebApi;
using System.ComponentModel.DataAnnotations;

namespace SoCarro.WebApi.ViewModels;

public class VeiculoViewModel : EntityViewModel 
{

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    //[StringLength(11, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
    public string Renavam { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Quilometragem { get;  set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal Valor { get;  set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Status { get;  set; }
    public DateTime DataCadastro { get;  set; }

    public ModeloViewModel Modelo { get;  set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid MarcaId { get;  set; }
    public MarcaViewModel Marca { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid ProprietarioId { get; set; }
    public ProprietarioViewModel Proprietario { get; set; }
 
}
