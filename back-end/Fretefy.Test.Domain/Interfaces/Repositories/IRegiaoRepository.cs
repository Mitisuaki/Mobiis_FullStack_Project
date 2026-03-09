using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository : IBaseRepository<Regiao>
    {
        Task<Regiao> SelecionarEntidadeAsyncComInclude(Expression<Func<Regiao, bool>> expression, CancellationToken cancellationToken = default);
        Task<List<Regiao>> SelecionarTodosAsyncComInclude(CancellationToken cancellationToken = default);
    }
}
