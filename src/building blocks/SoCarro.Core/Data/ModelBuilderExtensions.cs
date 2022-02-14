using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ConfigurarServidorBancoDados(this ModelBuilder modelBuilder, TipoServidorBancoDados tipoSGDB)
    {
        switch (tipoSGDB)
        {
            case TipoServidorBancoDados.SQLServer:
                foreach (var property in modelBuilder.Model.GetEntityTypes()
                             .SelectMany(e => e.GetProperties()
                                 .Where(p => p.ClrType == typeof(string))))
                    property.SetColumnType("varchar");
                return modelBuilder;

            case TipoServidorBancoDados.Postgres:
                modelBuilder.HasPostgresExtension("postgis");

                foreach (var property in modelBuilder.Model.GetEntityTypes()
                             .SelectMany(e => e.GetProperties()
                                 .Where(p => p.ClrType == typeof(string))))
                    property.SetColumnType("character varying");

                return modelBuilder;

            case TipoServidorBancoDados.MySQL:
                foreach (var property in modelBuilder.Model.GetEntityTypes()
                             .SelectMany(e => e.GetProperties()
                                 .Where(p => p.ClrType == typeof(string))))
                    property.SetColumnType("varchar");
                return modelBuilder;

            case TipoServidorBancoDados.Oracle:
                return modelBuilder;

            case TipoServidorBancoDados.SQLLite:
                return modelBuilder;

            default:
                return modelBuilder;
        }
    }

    public static ModelBuilder ConfigurarRelacionamentoEntidades(this ModelBuilder modelBuilder, DeleteBehavior behavior)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = behavior;

        return modelBuilder;
    }



}