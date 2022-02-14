using SoCarro.Business.Validations;
using SoCarro.Core.DomainObjects;

namespace SoCarro.Business.Models;

public class Endereco : Entity
{
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string Complemento { get; private set; }
    public string Bairro { get; private set; }
    public string Cep { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public Guid ProprietarioId { get; private set; }

    // EF Relation
    public Proprietario Proprietario { get; protected set; }

    public Endereco(string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid proprietarioId)
    {
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        ProprietarioId = proprietarioId;
    }

    public override bool EhValido()
    {
        return new EnderecoValidation().Validate(this).IsValid;
    }

    // EF Constructor
    protected Endereco() { }
}