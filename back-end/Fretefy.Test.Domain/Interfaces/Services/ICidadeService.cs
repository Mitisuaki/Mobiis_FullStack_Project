using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Services;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface ICidadeService : IBaseService<Cidade>
    {
        Task<Cidade> SelecionarEntidadeAsyncComInclude(Expression<Func<Cidade, bool>> expression, CancellationToken cancellationToken = default);
        Task<PagedResult<Cidade>> SelecionarPaginadoAsync(string nome, int page, int pageSize, CancellationToken cancellationToken);
    }
}
