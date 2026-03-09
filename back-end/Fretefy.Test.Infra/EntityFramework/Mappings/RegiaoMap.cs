using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RegiaoMap : IEntityTypeConfiguration<Regiao>
    {
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder.ToTable("REGIAO");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Ativo).HasColumnName("ATIVO").HasConversion<int>().IsRequired();

            builder.HasIndex(x => x.Nome).IsUnique();
        }
    }
}
