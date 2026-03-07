using Fretefy.Test.Application.Models.Regiao;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Application.Interfaces
{
    public interface IRegiaoAppService
    {
        bool Invalido { get; }
        IReadOnlyCollection<string> Mensagens { get; }
        Task<RegiaoDetalheDTO> ObterPorIdAsync(Guid regiaoId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RegiaoDTO>> ObterTodasAsync(CancellationToken cancellationToken = default);
        Task SalvarAsync(RegiaoInputModel regiao, CancellationToken cancellationToken = default);
        Task AtualizarAsync(Guid regiaoId, RegiaoInputModel regiao, CancellationToken cancellationToken = default);
        Task AtivarAsync(Guid id, CancellationToken cancellationToken);
        Task InativarAsync(Guid regiaoId, CancellationToken cancellationToken = default);
    }
}
