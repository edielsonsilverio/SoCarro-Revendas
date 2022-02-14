using SoCarro.Core.Notificacoes;

namespace SoCarro.Core.DomainObjects;

public interface INotificador
{
    bool TemNotificacao();
    List<Notificacao> ObterNotificacoes();
    void Manipulador(Notificacao notificacao);
}