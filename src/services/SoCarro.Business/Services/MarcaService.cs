using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Business.Validations;
using SoCarro.Core.DomainObjects;

namespace SoCarro.Business.Services;

public class MarcaService : BaseService, IMarcaService
{
    private readonly IMarcaRepository _marcaRepository;

    public MarcaService(IMarcaRepository marcaRepository,
                         INotificador notificador) : base(notificador)
    {
        _marcaRepository = marcaRepository;
    }

    public async Task<bool> Adicionar(Marca model)
    {
        if (!ExecutarValidacao(new MarcaValidation(), model)) return false;

        if (_marcaRepository.ObterTodos(f => f.Nome == model.Nome).Result.Any())
        {
            Notificar("Já existe um Marca com esta Descrição.");
            return false;
        }

        await _marcaRepository.Adicionar(model);
        return true;
    }

    public async Task<bool> Atualizar(Marca model)
    {
        if (!ExecutarValidacao(new MarcaValidation(), model)) return false;

        //if (_marcaRepository.ObterTodos(f => f.Nome != model.Nome && f.Id == model.Id).Result.Any())
        //{
        //    Notificar("Não é possível alterar o nome.");
        //    return false;
        //}

        if (_marcaRepository.ObterTodos(f => f.Nome == model.Nome && f.Id != model.Id).Result.Any())
        {
            Notificar("Já existe um Marca com esta Descrição.");
            return false;
        }

        await _marcaRepository.Atualizar(model);
        return true;
    }
    public void Dispose() => _marcaRepository?.Dispose();
}
