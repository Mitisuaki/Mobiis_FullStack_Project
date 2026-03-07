using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IRegiaoService : IBaseService<Regiao>, INotification
    {
        Task<Regiao> SelecionarEntidadeAsyncComInclude(Expression<Func<Regiao, bool>> expression, CancellationToken cancellationToken = default);
        Task<List<Regiao>> SelecionarTodosAsyncComInclude(CancellationToken cancellationToken = default);
        Task SalvarRegiaoAsync(string nome, List<Guid> cidadesIds, List<Guid> estadosIds, CancellationToken cancellationToken = default);
        Task AtualizarAsync(Guid regiaoId, string nome, List<Guid> cidadesIds, List<Guid> estadosIds, CancellationToken cancellationToken = default);
        Task AtivarAsync(Guid id, CancellationToken cancellationToken);
        Task InativarAsync(Guid regiaoId, CancellationToken cancellationToken = default);
    }
}
