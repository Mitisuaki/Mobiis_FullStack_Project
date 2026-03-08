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
            builder.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(50).IsRequired();
            builder.Property(p => p.EstadoId).HasColumnName("ESTADO_ID").HasMaxLength(2).IsRequired();

            builder.HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.EstadoId);

            builder.HasIndex(x => new {x.Nome, x.EstadoId}).IsUnique();
        }
    }
}
