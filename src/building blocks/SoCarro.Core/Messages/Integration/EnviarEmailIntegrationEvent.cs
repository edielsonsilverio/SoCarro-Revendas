namespace SoCarro.Core.Messages.Integration;

public  class EnviarEmailIntegrationEvent : IntegrationEvent
{
    public string Origem { get; private set; }
    public string Destino { get; private set; }
    public string Assunto { get; private set; }
    public string Mensagem { get; private set; }

    public EnviarEmailIntegrationEvent(string origem, string destino, string assunto, string mensagem)
    {
        Origem = origem;
        Destino = destino;
        Assunto = assunto;
        Mensagem = mensagem;
    }
}
