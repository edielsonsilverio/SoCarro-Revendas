using Microsoft.EntityFrameworkCore;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Core.Data;
using SoCarro.DataAccess.Context;
using System.Linq.Expressions;

namespace SoCarro.DataAccess.Repository;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly SoCarroDbContext _context;

    public VeiculoRepository(SoCarroDbContext context) => _context = context;
    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Veiculo>> ObterTodos(Expression<Func<Veiculo, bool>> filter = null)
    {
        //var query = _context.Veiculos.Include(x => x.Modelo);
        var query = _context.Veiculos;
        if (filter != null)
            return query.Where(filter);

        return query;
    }

    public async Task<Veiculo> ObterPorId(Guid modelId)
    {
        //return await _context.Veiculos.Include(x => x.Modelo).FirstOrDefaultAsync(x => x.Id == modelId);
        return await _context.Veiculos.FirstOrDefaultAsync(x => x.Id == modelId);

    }
    public async Task<bool> Atualizar(Veiculo model)
    {
        _context.Veiculos.Update(model);
        return Task.CompletedTask.IsCompleted;
    }
    public async Task<bool> Adicionar(Veiculo model)
    {
        await _context.Veiculos.AddAsync(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<Modelo> ObterModeloPorId(Guid id)
    {
        return await _context.Modelos.FirstOrDefaultAsync(x => x.VeiculoId == id);
    }

    public async Task<Proprietario> ObterProprietarioPorId(Guid id)
    {
        return await _context.Proprietarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> AtualizarModelo(Modelo model)
    {
        _context.Modelos.Update(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<bool> VerificarStatus(Guid modelId, TipoStatusVeiculo status)
    {
        return await _context.Veiculos.Where(x => x.Id == modelId && x.Status == 0).AnyAsync();
    }

    public async Task<bool> AtualizarMarca(Marca model)
    {
        _context.Marcas.Update(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<bool> AtualizarProprietario(Proprietario model)
    {
        _context.Proprietarios.Update(model);
        return Task.CompletedTask.IsCompleted;
    }
    public async Task<string> PersistirDados()
    {
        var result = await UnitOfWork.Commit();
        if (!result) return "Não foi possível persistir os dados no banco";

        return string.Empty;
    }
    public void Dispose() => _context.Dispose();
}