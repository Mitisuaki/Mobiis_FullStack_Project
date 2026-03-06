using Fretefy.Test.Infra.EntityFramework;
using Fretefy.Test.Domain.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            BaseDbContext context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
            try
            {
                logger.LogInformation(ProgramLogResource.IniciandoMigrations);
                await context.Database.MigrateAsync();
                logger.LogInformation(ProgramLogResource.SucessoMigrations);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, ProgramLogResource.ErroMigrations);
                throw;
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
