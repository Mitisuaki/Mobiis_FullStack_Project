using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> : INotification where TEntity : class
    {
        Task AdicionarAsync(TEntity entitie, CancellationToken cancellationToken = default);
        Task AdicionarListaAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> SalvarAsync(CancellationToken cancellationToken = default);
        Task<bool> ExisteEntidadeAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<bool> ExisteRegistrosAsync(CancellationToken cancellationToken = default);
        Task<TEntity> SelecionarEntidadeAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<List<TEntity>> SelecionarListaAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<List<TEntity>> SelecionarTodos(CancellationToken cancellationToken = default);
        Task<PagedResult<TEntity>> SelecionarPaginadoAsync(Expression<Func<TEntity, bool>> expression, 
                                                           int page, int pageSize, 
                                                           Expression<Func<TEntity, object>> orderBy, 
                                                           CancellationToken cancellationToken = default, 
                                                           params string[] includes);
    }
}
