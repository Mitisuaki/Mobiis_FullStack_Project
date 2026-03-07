using Fretefy.Test.Application.Interfaces;
using Fretefy.Test.Application.Models.Cidade;
using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Application.Services
{
    public class CidadeAppService : ICidadeAppService
    {
        private readonly ICidadeService _cidadeService;
        public bool Invalido => _cidadeService.Invalido;
        public IReadOnlyCollection<string> Mensagens => _cidadeService.Mensagens;

        public CidadeAppService(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        public async Task<CidadeDTO> ObterPorIdAsync(Guid cidadeId, CancellationToken cancellationToken = default)
        {
            Cidade cidade = await _cidadeService.SelecionarEntidadeAsyncComInclude(r => r.Id == cidadeId, cancellationToken);

            if (cidade == null)
            {
                return null;
            }

            return new CidadeDTO
            {
                Id = cidade.Id,
                Nome = cidade.Nome,
                EstadoId = cidade.EstadoId,
                Estado = cidade.Estado?.Sigla
            };
        }

        public async Task<PagedResult<CidadeDTO>> ObterTodasPaginadoAsync(string nome, int page, int pageSize, CancellationToken cancellationToken)
        {
            PagedResult<Cidade> pagedResult = await _cidadeService.SelecionarPaginadoAsync(nome, page, pageSize, cancellationToken);

            return new PagedResult<CidadeDTO>
            {
                TotalItems = pagedResult.TotalItems,
                Items = pagedResult.Items.Select(c => new CidadeDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    EstadoId = c.EstadoId,
                    Estado = c.Estado?.Sigla
                }).ToList()
            };
        }        
    }
}
