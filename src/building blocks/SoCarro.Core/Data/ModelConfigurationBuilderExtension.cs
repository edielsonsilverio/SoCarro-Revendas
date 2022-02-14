using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public static class ModelConfigurationBuilderExtension
{
    public static ModelConfigurationBuilder ConfigurarConvencaoTipoColuna(this ModelConfigurationBuilder configurationBuilder,
        TipoServidorBancoDados tipoSGDB)
    {
        configurationBuilder.Properties<string>()
            .AreUnicode(false);
        switch (tipoSGDB)
        {
            case TipoServidorBancoDados.SQLServer :
                configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("varchar");
                break;

            case TipoServidorBancoDados.Postgres:
                configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("character varying");
                break;

            case TipoServidorBancoDados.MySQL:
                configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("varchar");
                break;

            case TipoServidorBancoDados.Oracle:
                configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("varchar");
                break;

            case TipoServidorBancoDados.SQLLite:
                configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("text");
                break;

            default:
                break;
        }

        return configurationBuilder;
    }
}
