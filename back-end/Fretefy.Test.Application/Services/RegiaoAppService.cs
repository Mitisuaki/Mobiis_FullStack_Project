using Fretefy.Test.Application.Interfaces;
using Fretefy.Test.Application.Models.Cidade;
using Fretefy.Test.Application.Models.Estado;
using Fretefy.Test.Application.Models.Regiao;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Services;
using Fretefy.Test.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Application.Services
{
    public class RegiaoAppService : IRegiaoAppService
    {
        private readonly IRegiaoService _regiaoService;
        public bool Invalido => _regiaoService.Invalido;
        public IReadOnlyCollection<string> Mensagens => _regiaoService.Mensagens;

        public RegiaoAppService(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        public async Task<RegiaoDetalheDTO> ObterPorIdAsync(Guid regiaoId, CancellationToken cancellationToken = default)
        {
            Regiao regiao = await _regiaoService.SelecionarEntidadeAsyncComInclude(r => r.Id == regiaoId, cancellationToken);

            if (regiao == null) 
            {
                return null; 
            }

            return new RegiaoDetalheDTO
            {
                Id = regiao.Id,
                Nome = regiao.Nome,
                Ativo = regiao.Ativo,
                Cidades = regiao.RelacionamentosRegiaoCidadesUF
                    .Where(r => r.CidadeId.HasValue)
                    .Select(r => new CidadeDTO
                    {
                        Id = r.CidadeId.Value,
                        Nome = r.Cidade != null ? r.Cidade.Nome : string.Empty,
                        Estado = r.Cidade.Estado != null ? r.Cidade.Estado.Sigla : string.Empty,
                        EstadoId = r.EstadoId ?? Guid.Empty,
                    })
                    .ToList(),
                Estados = regiao.RelacionamentosRegiaoCidadesUF
                    .Where(r => r.EstadoId.HasValue)
                    .Select(r => new EstadoDTO
                    {
                        Id = r.EstadoId.Value,
                        Nome = r.Estado != null ? r.Estado.Nome : string.Empty,
                        Sigla = r.Estado != null ? r.Estado.Sigla : string.Empty,
                    })
                    .ToList()
            };
        }

        public async Task<IEnumerable<RegiaoDTO>> ObterTodasAsync(CancellationToken cancellationToken = default)
        {
            List<Regiao> regioes = await _regiaoService.SelecionarTodosAsyncComInclude(cancellationToken);

            List<RegiaoDTO> regioesDTO = new List<RegiaoDTO>();

            foreach (Regiao regiao in regioes)
            {
                RegiaoDTO dto = new RegiaoDTO
                {
                    Id = regiao.Id,
                    Nome = regiao.Nome,
                    Ativo = regiao.Ativo
                };

                if (regiao.RelacionamentosRegiaoCidadesUF != null)
                {
                    dto.Cidades = regiao.RelacionamentosRegiaoCidadesUF
                        .Where(r => r.Cidade != null)
                        .Select(r => r.Cidade.Nome)
                        .ToList();

                    dto.Estados = regiao.RelacionamentosRegiaoCidadesUF
                        .Where(r => r.Estado != null)
                        .Select(r => r.Estado.Sigla)
                        .ToList();
                }

                regioesDTO.Add(dto);
            }

            return regioesDTO;
        }

        public async Task SalvarAsync(RegiaoInputModel regiao, CancellationToken cancellationToken = default)
        {
            await _regiaoService.SalvarRegiaoAsync(regiao.Nome, regiao.CidadesIds, regiao.EstadosIds, cancellationToken);
        }

        public async Task AtualizarAsync(Guid regiaoId, RegiaoInputModel regiao, CancellationToken cancellationToken = default)
        {
            await _regiaoService.AtualizarAsync(regiaoId, regiao.Nome, regiao.CidadesIds, regiao.EstadosIds, cancellationToken);
        }

        public async Task AtivarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _regiaoService.AtivarAsync(id, cancellationToken);
        }

        public async Task InativarAsync(Guid regiaoId, CancellationToken cancellationToken = default)
        {
            await _regiaoService.InativarAsync(regiaoId, cancellationToken);
        }

        public async Task ExcluirEntidade(Guid regiaoId, CancellationToken cancellationToken = default)
        {
            Regiao regiao = await _regiaoService.SelecionarEntidadeAsyncComInclude(r => r.Id == regiaoId, cancellationToken);

            if (regiao == null)
            {
                _regiaoService.AdicionarMensagem(MensagensRegiaoServiceResource.RegiaoServiceRegiaoNaoEncontrada);
                return;
            }

            await _regiaoService.ExcluirEntidade(regiao, cancellationToken);
        }
    }
}
