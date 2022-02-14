using Core.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoCarro.Business.Models;

namespace SoCarro.DataAccess.Mappings;

public class EnderecoMapping : EntityMapping<Endereco>
{
    private static string _nameTable = nameof(Endereco);
    public EnderecoMapping() : base(_nameTable) { }
    public override void Configure(EntityTypeBuilder<Endereco> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Logradouro).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Numero).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Cep).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Complemento).HasMaxLength(250);
        builder.Property(x => x.Bairro).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Cidade).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Estado).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ProprietarioId).HasMaxLength(36);

        //builder.HasData(
        //    new Endereco("Rua 10", "01", "Quadra 15", "Centro", "78098000", "Cuiabá", "MT", Guid.Empty),
        //    new Endereco("Rua Brazil", "10", "Quadra 36", "Porto", "78098000", "Cuiabá", "MT", Guid.Empty),
        //    new Endereco("Rua Orquideas", "01", "Quadra 98", "Jardim Universitário", "78098000", "Cuiabá", "MT", Guid.Empty),
        //    new Endereco("Rua 42", "395", "Quadra 70", "Jardim Imperial", "78098000", "Cuiabá", "MT", Guid.Empty)
        //    );
    }
}
