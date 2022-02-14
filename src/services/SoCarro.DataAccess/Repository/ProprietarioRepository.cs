using Microsoft.EntityFrameworkCore;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Core.Data;
using SoCarro.DataAccess.Context;
using System.Linq.Expressions;

namespace SoCarro.DataAccess.Repository;

public class ProprietarioRepository : IProprietarioRepository
{
    private readonly SoCarroDbContext _context;

    public ProprietarioRepository(SoCarroDbContext context) => _context = context;
    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Proprietario>> ObterTodos(Expression<Func<Proprietario, bool>> filter = null)
    {
        var query = _context.Proprietarios.Include(x=> x.Endereco);

        if (filter != null)
            return query.Where(filter);

        return query;
    }

    public async Task<Proprietario> ObterPorId(Guid modelId)
    {
        var query = await _context.Proprietarios.Include(x => x.Endereco).FirstOrDefaultAsync(x => x.Id == modelId);
        return query;
    }
    public async Task<bool> Atualizar(Proprietario model)
    {
        _context.Proprietarios.Update(model);
        return Task.CompletedTask.IsCompleted;
    }
    public async Task<bool> Adicionar(Proprietario model)
    {
        await _context.Proprietarios.AddAsync(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<Endereco> ObterEndereco(Guid id)
    {
        return await _context.Enderecos.FirstOrDefaultAsync(x => x.ProprietarioId == id);
    }

    public async Task<bool> AtualizarEndereco(Endereco model)
    {
        _context.Enderecos.Update(model);
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
