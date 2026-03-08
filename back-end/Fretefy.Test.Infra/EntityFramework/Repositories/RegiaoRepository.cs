using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoRepository : BaseRepository<Regiao>, IRegiaoRepository
    {
        public RegiaoRepository(DbContext dbContext) : base(dbContext) { }

        public async Task<Regiao> SelecionarEntidadeAsyncComInclude(Expression<Func<Regiao, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(r => r.RelacionamentosRegiaoCidadesUF)
                               .ThenInclude(r => r.Cidade)
                               .ThenInclude(r => r.Estado)
                               .Include(r => r.RelacionamentosRegiaoCidadesUF)
                               .ThenInclude(r => r.Estado)
                               .FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<List<Regiao>> SelecionarTodosAsyncComInclude(CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(r => r.RelacionamentosRegiaoCidadesUF)
                               .ThenInclude(r => r.Cidade)
                               .Include(r => r.RelacionamentosRegiaoCidadesUF)
                               .ThenInclude(r => r.Estado)
                               .ToListAsync(cancellationToken);
        }
    }
}
