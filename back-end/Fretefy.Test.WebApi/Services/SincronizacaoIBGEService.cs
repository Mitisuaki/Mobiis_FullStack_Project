using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Services
{
    internal class SincronizacaoIBGEService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SincronizacaoIBGEService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            ISincronizacaoGeograficaService sincronizacaoCidadeService = scope.ServiceProvider.GetRequiredService<ISincronizacaoGeograficaService>();
            await sincronizacaoCidadeService.SincronizarCidadesEUFIBGEAsync(cancellationToken);
        }
    }
}
