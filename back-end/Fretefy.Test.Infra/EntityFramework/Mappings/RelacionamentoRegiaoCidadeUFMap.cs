using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RelacionamentoRegiaoCidadeUFMap : IEntityTypeConfiguration<RelacionamentoRegiaoCidadeUF>
    {
        public void Configure(EntityTypeBuilder<RelacionamentoRegiaoCidadeUF> builder)
        {
            builder.ToTable("REL_REGIAO_CIDADE");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
            builder.Property(x => x.RegiaoId).HasColumnName("REGIAO_ID").IsRequired();
            builder.Property(x => x.CidadeId).HasColumnName("CIDADE_ID").IsRequired(false);
            builder.Property(x => x.EstadoId).HasColumnName("ESTADO_ID").IsRequired(false);

            builder.HasOne(x => x.Regiao).WithMany().HasForeignKey(x => x.RegiaoId);
            builder.HasOne(x => x.Cidade).WithMany().HasForeignKey(x => x.CidadeId);
            builder.HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.EstadoId);

            builder.HasIndex(x => new { x.RegiaoId, x.CidadeId })
                   .IsUnique()
                   .HasFilter("\"CIDADE_ID\" IS NOT NULL");

            builder.HasIndex(x => new { x.RegiaoId, x.EstadoId })
                   .IsUnique()
                   .HasFilter("\"ESTADO_ID\" IS NOT NULL");
        }
    }
}
