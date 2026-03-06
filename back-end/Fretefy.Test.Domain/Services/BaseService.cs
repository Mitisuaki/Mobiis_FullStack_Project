using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _repository;

        protected BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task AdicionarListaCidadesAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _repository.AdicionarListaAsync(entities, cancellationToken);
        }

        public async Task<int> SalvarAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.SalvarAsync(cancellationToken);
        }

        public async Task<TEntity> SelecionarEntidadeAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.SelecionarEntidadeAsync(expression);
        }

        public async Task<List<TEntity>> SelecionarListaAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.SelecionarListaAsync(expression);
        }

        public IQueryable<TEntity> SelecionarTodos()
        {
            return _repository.SelecionarTodos();
        }
    }
}
