using Microsoft.EntityFrameworkCore;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Core.Data;
using SoCarro.DataAccess.Context;
using System.Linq.Expressions;

namespace SoCarro.DataAccess.Repository;

public class MarcaRepository : IMarcaRepository
{
    private readonly SoCarroDbContext _context;

    public MarcaRepository(SoCarroDbContext context) => _context = context;
    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Marca>> ObterTodos(Expression<Func<Marca, bool>> filter = null)
    {
        var query = _context.Marcas;

        if (filter != null)
           return  await query.Where(filter).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<Marca> ObterPorId(Guid modelId)
    {
        return await _context.Marcas.FindAsync(modelId);
    }

    public async Task<bool> Adicionar(Marca model)
    {
        await _context.Marcas.AddAsync(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<bool> Atualizar(Marca model)
    {
        _context.Marcas.Update(model);
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
