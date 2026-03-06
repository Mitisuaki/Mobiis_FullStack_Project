using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public static class MapeamentoEntidadesConfig
    {
        public static void AdicionarMapeamentoEntidades(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadeMap());
        }
    }
}
