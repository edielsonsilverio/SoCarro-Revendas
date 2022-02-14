using Core.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoCarro.Business.Models;

namespace SoCarro.DataAccess.Mappings;

public class ModeloMapping : EntityMapping<Modelo>
{
    private static string _nameTable = nameof(Modelo);
    public ModeloMapping() : base(_nameTable) { }
    public override void Configure(EntityTypeBuilder<Modelo> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Descricao).HasMaxLength(50);
        builder.Property(x => x.AnoModelo);
        builder.Property(x => x.AnoFabricacao);
        builder.Property(x => x.VeiculoId).HasMaxLength(36);

        //builder.HasData(
        //    new Modelo("Trend",2020,2021, Guid.Empty),
        //    new Modelo("Confort line", 2021, 2021, Guid.Empty),
        //    new Modelo("Verão", 2021, 2022, Guid.Empty),
        //    new Modelo("Inferno", 2021, 2022, Guid.Empty),
        //    new Modelo("Esportivo", 2022, 2022, Guid.Empty)
        //);
    }
}

