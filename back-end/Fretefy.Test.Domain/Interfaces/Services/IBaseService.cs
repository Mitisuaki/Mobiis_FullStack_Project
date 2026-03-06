using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task AdicionarListaCidadesAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> SalvarAsync(CancellationToken cancellationToken = default);
        Task<TEntity> SelecionarEntidadeAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> SelecionarListaAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> SelecionarTodos();
    }
}
