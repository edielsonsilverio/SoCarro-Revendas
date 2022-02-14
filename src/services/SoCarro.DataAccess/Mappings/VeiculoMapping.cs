using Core.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoCarro.Business.Models;
using SoCarro.Business.Validations.Documentos;

namespace SoCarro.DataAccess.Mappings;

public class VeiculoMapping : EntityMapping<Veiculo>
{
    private static string _nameTable = nameof(Veiculo);
    public VeiculoMapping() : base(_nameTable) { }
    public override void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Renavam).HasMaxLength(RenavamValidacao.TamanhoRenavam);
        builder.Property(x => x.MarcaId).HasMaxLength(36);
        builder.Property(x => x.ProprietarioId).HasMaxLength(36);
        builder.Property(x => x.Quilometragem);
        builder.Property(x => x.Valor).HasPrecision(18,2);

        builder.Property(x => x.Status);

        // 1 : 1 => Veiculo : Modelo
        builder.HasOne(c => c.Modelo);
        //.WithOne(c => c.Veiculo)

        // 1 : 1 => Veiculo : Proprietario
        builder.HasOne(c => c.Proprietario);
        //.WithOne(c => c.Veiculo)

        // 1 : 1 => Veiculo : Marca
        builder.HasOne(c => c.Marca);
        //.WithOne(c => c.Veiculo)

    }
}