using Fretefy.Test.Application.Interfaces;
using Fretefy.Test.Application.Services;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Gateways;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using Fretefy.Test.Domain.Services;
using Fretefy.Test.Infra.EntityFramework;
using Fretefy.Test.Infra.EntityFramework.Repositories;
using Fretefy.Test.Infra.Gateways;
using Fretefy.Test.Infra.Services;
using Fretefy.Test.WebApi.Filters;
using Fretefy.Test.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using static Fretefy.Test.Infra.Constantes.InfraConstants;
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
            services.AddScoped<ICidadeAppService, CidadeAppService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IEstadoAppService, EstadoAppService>();
            services.AddScoped<IRegiaoService, RegiaoService>();
            services.AddScoped<IRegiaoAppService, RegiaoAppService>();
            services.AddScoped<ISincronizacaoGeograficaService, SincronizacaoGeograficaService>();
            services.AddScoped<IExportacaoService, ExportacaoService>();
        }

        public static void InjetarBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<SincronizacaoIBGEService>();
        }

        public static void InjetarSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Swagger.Version, new OpenApiInfo { Title = Swagger.Title, Version = Swagger.Version });
                c.OperationFilter<AutoMethodNameOperationFilter>();
            });
        }
    }
}
