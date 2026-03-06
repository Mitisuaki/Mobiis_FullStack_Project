using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class CidadeMap : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("CIDADE");

            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
            builder.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(250).IsRequired();
            builder.Property(p => p.UF).HasColumnName("UF").HasMaxLength(2).IsRequired();
        }
    }
}
