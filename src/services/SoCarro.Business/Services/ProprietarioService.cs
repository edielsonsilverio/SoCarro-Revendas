using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Business.Validations;
using SoCarro.Core.DomainObjects;

namespace SoCarro.Business.Services;

public class ProprietarioService : BaseService, IProprietarioService
{
    private readonly IProprietarioRepository _proprietarioRepository;
    
    public ProprietarioService(IProprietarioRepository proprietarioRepository,
                               INotificador notificador) : base(notificador)
    {
        _proprietarioRepository = proprietarioRepository;
    }

    public async Task<bool> Adicionar(Proprietario proprietario)
    {
         
        if (!ExecutarValidacao(new ProprietarioValidation(), proprietario)
            || !ExecutarValidacao(new EnderecoValidation(), proprietario.Endereco)) return false;

        if (_proprietarioRepository.ObterTodos(f => f.Documento == proprietario.Documento).Result.Any())
        {
            Notificar("Já existe um Proprietario com este documento informado.");
            return false;
        }

        await _proprietarioRepository.Adicionar(proprietario);
        return true;
    }

    public async Task<bool> Atualizar(Proprietario proprietario)
    {
        if (!ExecutarValidacao(new ProprietarioValidation(), proprietario)) return false;


        if (_proprietarioRepository.ObterTodos(f => f.Nome != proprietario.Nome && f.Id == proprietario.Id).Result.Any())
        {
            Notificar("Não é possível alterar o nome.");
            return false;
        }

        if (_proprietarioRepository.ObterTodos(f => f.Documento == proprietario.Documento && f.Id != proprietario.Id).Result.Any())
        {
            Notificar("Já existe um Proprietario com este documento infomado.");
            return false;
        }

        await _proprietarioRepository.Atualizar(proprietario);
        return true;
    }

    public async Task AtualizarEndereco(Endereco endereco)
    {
        if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

        await _proprietarioRepository.AtualizarEndereco(endereco);
    }

    public void Dispose() => _proprietarioRepository?.Dispose();
}
