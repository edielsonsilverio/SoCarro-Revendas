using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public static class OptionsBuilderExtenstions
{
    public static DbContextOptionsBuilder ConfigurarConexao(this DbContextOptionsBuilder optionsBuilder,
        TipoServidorBancoDados tipoSGDB,
        string stringConexao)
    {
        switch (tipoSGDB)
        {
            case TipoServidorBancoDados.SQLServer:
                return optionsBuilder.UseSqlServer(stringConexao);

            case TipoServidorBancoDados.Postgres:
                return optionsBuilder
                    .UseNpgsql(stringConexao,
                        x => x.SetPostgresVersion(new Version(9, 5)));
          
            case TipoServidorBancoDados.MySQL:
                return optionsBuilder.UseMySql(stringConexao,new MySqlServerVersion(new Version(5,7)));

            case TipoServidorBancoDados.Oracle:
                break;
            case TipoServidorBancoDados.SQLLite:
                break;
            default:
                break;
        }
        return optionsBuilder;
    }
}