using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface ICidadeRepository : IBaseRepository<Cidade>
    {
        Task<Cidade> SelecionarEntidadeAsyncComInclude(Expression<Func<Cidade, bool>> expression, CancellationToken cancellationToken = default);
    }
}
