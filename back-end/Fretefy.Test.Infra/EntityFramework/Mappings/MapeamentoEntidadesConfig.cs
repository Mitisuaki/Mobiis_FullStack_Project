using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public static class MapeamentoEntidadesConfig
    {
        public static void AdicionarMapeamentoEntidades(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadeMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new RegiaoMap());
            modelBuilder.ApplyConfiguration(new RelacionamentoRegiaoCidadeUFMap());
        }
    }
}
