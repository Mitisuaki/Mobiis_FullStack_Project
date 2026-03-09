using Fretefy.Test.Application.Models.Cidade;
using Fretefy.Test.Application.Models.Regiao;
using Fretefy.Test.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Application.Interfaces
{
    public interface ICidadeAppService
    {
        bool Invalido { get; }
        IReadOnlyCollection<string> Mensagens { get; }
        Task<CidadeDTO> ObterPorIdAsync(Guid cidadeId, CancellationToken cancellationToken = default);
        Task<PagedResult<CidadeDTO>> ObterTodasPaginadoAsync(string nome, int page, int pageSize, Guid[] estadosIgnorados = null, CancellationToken cancellationToken = default);
        Task<List<CidadeDTO>> SelecionarTodas(CancellationToken cancellationToken = default);
    }
}
