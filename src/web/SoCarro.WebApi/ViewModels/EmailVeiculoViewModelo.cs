namespace SoCarro.WebApi.ViewModels;

public class EmailVeiculoViewModel
{
    public string Origem { get; set; }
    public string Destino { get; set; }
    public string Assunto { get; set; }
    public string Mensagem { get; set; }
    public VeiculoViewModel Veiculo { get; set; }

    public EmailVeiculoViewModel()
    {
        Veiculo = new VeiculoViewModel();
    }

}
