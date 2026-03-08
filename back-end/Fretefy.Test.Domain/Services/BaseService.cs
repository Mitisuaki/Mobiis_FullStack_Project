using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using Fretefy.Test.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public abstract class BaseService<TEntity> : Notification, IBaseService<TEntity> where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _repository;

        protected BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task AdicionarAsync(TEntity entitie, CancellationToken cancellationToken = default)
        {
            await _repository.AdicionarAsync(entitie, cancellationToken);
        }

        public virtual async Task AdicionarListaAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _repository.AdicionarListaAsync(entities, cancellationToken);
        }

        public virtual async Task<int> SalvarAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.SalvarAsync(cancellationToken);
        }

        public virtual async Task<bool> ExisteEntidadeAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _repository.ExisteEntidadeAsync(expression, cancellationToken);
        }
        public virtual async Task<bool> ExisteRegistrosAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.ExisteRegistrosAsync(cancellationToken);
        }

        public virtual async Task<TEntity> SelecionarEntidadeAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _repository.SelecionarEntidadeAsync(expression, cancellationToken);
        }

        public virtual async Task<List<TEntity>> SelecionarListaAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _repository.SelecionarListaAsync(expression, cancellationToken);
        }

        public virtual async Task<List<TEntity>> SelecionarTodos(CancellationToken cancellationToken = default)
        {
            return await _repository.SelecionarTodos(cancellationToken);
        }

        public virtual async Task<PagedResult<TEntity>> SelecionarPaginadoAsync(Expression<Func<TEntity, bool>> expression, 
                                                                                int page, int pageSize, 
                                                                                Expression<Func<TEntity, object>> orderBy, 
                                                                                CancellationToken cancellationToken = default, 
                                                                                params string[] includes)
        {
            return await _repository.SelecionarPaginadoAsync(expression, page, pageSize, orderBy, cancellationToken, includes);
        }
    }
}
