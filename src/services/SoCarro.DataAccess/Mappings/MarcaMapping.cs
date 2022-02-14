using Core.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoCarro.Business.Models;

namespace SoCarro.DataAccess.Mappings;

public class MarcaMapping : EntityMapping<Marca>
{
    private static string _nameTable = nameof(Marca);
    public MarcaMapping() : base(_nameTable) { }
    public override void Configure(EntityTypeBuilder<Marca> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasMaxLength(50);
        builder.Property(x => x.Status);

        builder.HasData( 
            new Marca("Volkswagen"),
            new Marca("Toyota"),
            new Marca("Ford"),
            new Marca("Honda"),
            new Marca("Hyundai")
         );
    }
}
