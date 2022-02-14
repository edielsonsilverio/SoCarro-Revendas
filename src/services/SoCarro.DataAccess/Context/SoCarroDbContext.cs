using Core.Data;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SoCarro.Business.Models;
using SoCarro.Core;
using SoCarro.Core.Data;
using SoCarro.Core.DomainObjects;
using SoCarro.Core.Mediator;
using SoCarro.Core.Messages;

namespace SoCarro.DataAccess.Context;

public class SoCarroDbContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    //public SoCarroDbContext(){}
    public SoCarroDbContext(DbContextOptions<SoCarroDbContext> options,
                            IMediatorHandler mediatorHandler) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;

        _mediatorHandler = mediatorHandler;
    }

    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Proprietario> Proprietarios { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Modelo> Modelos { get; set; }


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigurarConvencaoTipoColuna(TipoServidorBancoDados.SQLServer);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.ConfigurarConexao(TipoServidorBancoDados.SQLServer, GlobalConstants.CONEXAO_BANCO_SQLSERVER);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigurarRelacionamentoEntidades(DeleteBehavior.ClientSetNull);

        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SoCarroDbContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
 
            if (entry.State == EntityState.Modified)
                entry.Property("DataCadastro").IsModified = false;
        }
        try
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this);
            return sucesso;
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            return false;
        }
        
    }
}

public static class MediatorExtension
{
    public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.PublicarEvento(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}
/*
    
    Add-Migration TabelasIniciais -StartupProject DataAccess\SoCarro.DataAccess -Project DataAccess\SoCarro.DataAccess
   
    Update-Database -StartupProject DataAccess\SoCarro.DataAccess -Project DataAccess\SoCarro.DataAccess

    DROP TABLE Modelo
    GO


    DROP TABLE Veiculo
    GO


    DROP TABLE Endereco
    GO


    DROP TABLE Proprietario
    GO


    DROP TABLE Marca
    GO

    DELETE FROM  __EFMigrationsHistory WHERE MigrationId = '20220205203630_TabelasUsuario'

*/