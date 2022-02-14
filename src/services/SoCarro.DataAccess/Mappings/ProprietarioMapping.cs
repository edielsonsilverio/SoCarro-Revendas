using Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoCarro.Business.Models;
using SoCarro.Core.DomainObjects;

namespace SoCarro.DataAccess.Mappings;

public class ProprietarioMapping : EntityMapping<Proprietario>
{
    private static string _nameTable = nameof(Proprietario);
    public ProprietarioMapping() : base(_nameTable) { }
    public override void Configure(EntityTypeBuilder<Proprietario> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasMaxLength(50);
        builder.Property(x => x.Documento).HasMaxLength(14);
        builder.Property(x => x.TipoProprietario);
        builder.Property(x => x.Status);


        builder.OwnsOne(c => c.Email, tf =>
        {
            tf.Property(c => c.Endereco)
                .HasColumnName(nameof(Email))
                .IsRequired()
                .HasMaxLength(Email.EnderecoMaxLength);
        });

        // 1 : 1 => Proprietario : Endereco
        builder.HasOne(c => c.Endereco);
    }
}
