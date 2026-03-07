using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class CidadeRepository : BaseRepository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(DbContext dbContext) : base(dbContext) { }

        public async Task<Cidade> SelecionarEntidadeAsyncComInclude(Expression<Func<Cidade, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(r => r.Estado)
                               .FirstOrDefaultAsync(expression, cancellationToken);
        }
    }
}
