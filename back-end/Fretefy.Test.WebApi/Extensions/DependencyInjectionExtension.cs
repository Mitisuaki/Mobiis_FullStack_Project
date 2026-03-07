using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Gateways;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using Fretefy.Test.Domain.Services;
using Fretefy.Test.Infra.EntityFramework;
using Fretefy.Test.Infra.EntityFramework.Repositories;
using Fretefy.Test.Infra.Gateways;
using Fretefy.Test.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Fretefy.Test.Infra.Constantes.InfraConstants.ConnectionStrings;

namespace Fretefy.Test.WebApi.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void InjetarDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, BaseDbContext>();
            services.AddDbContext<BaseDbContext>( options =>
            {
                options.UseSqlite(configuration.GetConnectionString(DefaultConnection));
            });
        }

        public static void InjetarRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IRegiaoRepository, RegiaoRepository>();
        }

        public static void InjetarGateways(this IServiceCollection services)
        {
            services.AddHttpClient<IIBGEGateway, IBGEGateway>();
        }

        public static void InjetarServicos(this IServiceCollection services)
        {
            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<ISincronizacaoGeograficaService, SincronizacaoGeograficaService>();
            services.AddScoped<IRegiaoService, RegiaoService>();
        }

        public static void InjetarBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<SincronizacaoIBGEService>();
        }
    }
}
