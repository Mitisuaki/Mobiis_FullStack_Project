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
        private readonly IIBGEGateway _ibgeGateway;
        private readonly IEstadoService _estadoService;
        private readonly ILogger<SincronizacaoGeograficaService> _logger;

        public SincronizacaoGeograficaService(ICidadeService cidadeService,
                                          IIBGEGateway ibgeGateway,
                                          IEstadoService estadoService,
                                          ILogger<SincronizacaoGeograficaService> logger)
        {
            _cidadeService = cidadeService;
            _ibgeGateway = ibgeGateway;
            _estadoService = estadoService;
            _logger = logger;
        }

        public async Task SincronizarCidadesEUFIBGEAsync(CancellationToken cancellationToken)
        {
            List<Cidade> cidadesExistentes = await _cidadeService.SelecionarTodos(cancellationToken);

            if (cidadesExistentes.Any())
            {
                return;
            }

            try
            {
                _logger.LogInformation(SincronizacaoGeograficaServiceLogResource.InicializacaoIBGE);
                List<MunicipioIBGEDTO> cidadesIBGE = await _ibgeGateway.ObterCidadesComUFAsync(cancellationToken);
                List<Estado> estados = await _estadoService.SelecionarTodos(cancellationToken);

                foreach (MunicipioIBGEDTO cidadeIBGE in cidadesIBGE)
                {
                    Estado estado = estados.Find(x => x.Sigla == cidadeIBGE.EstadoSigla);

                    if (estado == null)
                    {
                        estado = new Estado(cidadeIBGE.EstadoNome, cidadeIBGE.EstadoSigla);

                        if (estado.Invalido)
                        {
                            AdicionarRangeMensagens(estado.Mensagens);
                            continue;
                        }

                        await _estadoService.AdicionarAsync(estado, cancellationToken);                        
                    }

                    Cidade cidade = new Cidade(cidadeIBGE.CidadeNome, estado.Id);
                    if (cidade.Invalido)
                    {
                        AdicionarRangeMensagens(cidade.Mensagens);
                        continue;
                    }

                    await _cidadeService.AdicionarAsync(cidade, cancellationToken);
                    _logger.LogInformation(SincronizacaoGeograficaServiceLogResource.SucessoIBGE);
                }


                if (!Invalido)
                {                    
                    await _cidadeService.SalvarAsync();
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
