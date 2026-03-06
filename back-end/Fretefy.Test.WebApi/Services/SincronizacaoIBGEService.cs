using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Resources;
using Fretefy.Test.Infra.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static Fretefy.Test.Infra.Constantes.InfraConstants.APIsExternas;

namespace Fretefy.Test.WebApi.Services
{
    internal class SincronizacaoIBGEService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public SincronizacaoIBGEService(IServiceProvider serviceProvider, 
                                        ILogger<SincronizacaoIBGEService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            ICidadeService cidadeService = scope.ServiceProvider.GetRequiredService<ICidadeService>();
            IQueryable<Cidade> cidades = cidadeService.SelecionarTodos();

            try
            {
                if (!cidades.Any())
                {
                    _logger.LogInformation(SincronizacaoIBGELogResource.InicializacaoIBGE);

                    using HttpClient httpClient = new HttpClient();
                    HttpResponseMessage resposta = await httpClient.GetAsync(IBGEMunicipios, cancellationToken);

                    resposta.EnsureSuccessStatusCode();

                    Stream stream = await resposta.Content.ReadAsStreamAsync();
                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    List<IBGEMunicipioDTO> municipiosIBGE = await JsonSerializer.DeserializeAsync<List<IBGEMunicipioDTO>>(stream, jsonOptions, cancellationToken);

                    if (municipiosIBGE != null && !municipiosIBGE.Any())
                    {
                        List<Cidade> cidadesIBGE = municipiosIBGE.Select(m => new Cidade(m.Nome, m.Microrregiao.Mesorregiao.UF.Sigla)).ToList();

                        await cidadeService.AdicionarListaCidadesAsync(cidadesIBGE, cancellationToken);
                        await cidadeService.SalvarAsync(cancellationToken);

                        _logger.LogInformation(SincronizacaoIBGELogResource.SucessoIBGE);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SincronizacaoIBGELogResource.ErroIBGE);
            }
        }
    }
}
