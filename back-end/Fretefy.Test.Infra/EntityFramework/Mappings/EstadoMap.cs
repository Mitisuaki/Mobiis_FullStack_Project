using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class EstadoMap : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("ESTADO");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnName("NOME").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Sigla).HasColumnName("SIGLA").HasMaxLength(2).IsRequired();

            builder.HasIndex(x => x.Nome).IsUnique();
            builder.HasIndex(x => x.Sigla).IsUnique();
        }
    }
}
