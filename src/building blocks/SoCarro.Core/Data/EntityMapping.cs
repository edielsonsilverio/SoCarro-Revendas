using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoCarro.Core.DomainObjects;

namespace Core.Data;

public abstract class EntityMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    private readonly string _tableName;
    protected int casaInicial = 18;
    protected int casaFinal = 2;
    protected EntityMapping(string tableName = "") => _tableName = tableName;

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        if (!string.IsNullOrEmpty(_tableName))
            builder.ToTable(_tableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(36);
    }
}