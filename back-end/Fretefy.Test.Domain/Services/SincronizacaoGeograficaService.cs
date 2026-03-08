using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Gateways;
using Fretefy.Test.Domain.Interfaces.Services;
using Fretefy.Test.Domain.Notifications;
using Fretefy.Test.Domain.Resources;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public class SincronizacaoGeograficaService : Notification, ISincronizacaoGeograficaService
    {
        private readonly ICidadeService _cidadeService;
        private readonly IEstadoService _estadoService;
        private readonly IIBGEGateway _ibgeGateway;
        private readonly ILogger<SincronizacaoGeograficaService> _logger;

        public SincronizacaoGeograficaService(IIBGEGateway ibgeGateway,
                                              IEstadoService estadoService,
                                              ICidadeService cidadeService,
                                              ILogger<SincronizacaoGeograficaService> logger)
        {
            _cidadeService = cidadeService;
            _estadoService = estadoService;
            _ibgeGateway = ibgeGateway;
            _logger = logger;
        }

        public async Task SincronizarCidadesEUFIBGEAsync(CancellationToken cancellationToken = default)
        {
            bool cidadesAny = await _cidadeService.ExisteRegistrosAsync(cancellationToken);
            bool estadosAny = await _estadoService.ExisteRegistrosAsync(cancellationToken);

            if (cidadesAny && estadosAny)
            {
                return;
            }

            try
            {
                _logger.LogInformation(SincronizacaoGeograficaServiceLogResource.InicializacaoIBGE);

                List<EstadoIBGEDTO> estadosIBGE = await _ibgeGateway.ObterEstadosAsync(cancellationToken);

                if (!estadosAny)
                {
                    foreach (EstadoIBGEDTO estadoIBGE in estadosIBGE)
                    {

                        Estado novoEstado = new Estado(estadoIBGE.Nome, estadoIBGE.Sigla);

                        if (novoEstado.Invalido)
                        {
                            AdicionarRangeMensagens(novoEstado.Mensagens);
                            continue;
                        }

                        await _estadoService.AdicionarAsync(novoEstado, cancellationToken);
                    }

                     await _estadoService.SalvarAsync();
                }

                List<MunicipioIBGEDTO> cidadesIBGE = await _ibgeGateway.ObterCidadesAsync(cancellationToken);
                List<Estado> estadosBD = await _estadoService.SelecionarTodos(cancellationToken);

                Dictionary<int, Guid> dicionarioEstados = estadosIBGE.Join(estadosBD,
                                                                           ibge => ibge.Sigla,
                                                                           bd => bd.Sigla,
                                                                           (ibge, bd) => new { IdIBGE = ibge.Id, IdBD = bd.Id })
                                                                     .ToDictionary(k => k.IdIBGE, v => v.IdBD);

                foreach (MunicipioIBGEDTO cidadeIBGE in cidadesIBGE)
                {
                    int idEstadoIbge = int.Parse(cidadeIBGE.Id.ToString()[..2]);

                    if (!dicionarioEstados.TryGetValue(idEstadoIbge, out Guid estadoId))
                    {
                        continue;
                    }

                    Cidade novaCidade = new Cidade(cidadeIBGE.Nome, estadoId);

                    if (novaCidade.Invalido)
                    {
                        AdicionarRangeMensagens(novaCidade.Mensagens);
                        continue;
                    }

                    await _cidadeService.AdicionarAsync(novaCidade, cancellationToken);
                }

                await _cidadeService.SalvarAsync();

                if (!Invalido)
                {                    
                    _logger.LogInformation(SincronizacaoGeograficaServiceLogResource.SucessoIBGE);
                }
                else
                {
                    _logger.LogWarning(SincronizacaoGeograficaServiceLogResource.WarningIBGE, string.Join(", ", Mensagens));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SincronizacaoGeograficaServiceLogResource.ErroIBGE);
            }
        }
    }
}
