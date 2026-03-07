using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task AdicionarAsync(TEntity entitie, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entitie, cancellationToken);
        }

        public virtual async Task AdicionarListaAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task<int> SalvarAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> SelecionarEntidadeAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(expression, cancellationToken);
        }

        public virtual async Task<List<TEntity>> SelecionarListaAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(expression).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> SelecionarTodos(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<PagedResult<TEntity>> SelecionarPaginadoAsync(Expression<Func<TEntity, bool>> expression,
                                                                                int page, int pageSize, 
                                                                                Expression<Func<TEntity, object>> orderBy,
                                                                                CancellationToken cancellationToken = default,
                                                                                params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (includes != null && includes.Any())
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            int total = await query.CountAsync(cancellationToken);

            List<TEntity> items = await query.OrderBy(orderBy)
                                             .Skip((page - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToListAsync(cancellationToken);

            return new PagedResult<TEntity>
            {
                TotalItems = total,
                Items = items
            };
        }
    }
}
