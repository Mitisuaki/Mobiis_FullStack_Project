using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static Fretefy.Test.Domain.Constantes.DominioConstants;

namespace Fretefy.Test.Domain.Services
{
    public class CidadeService : BaseService<Cidade>, ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository) : base(cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public async Task<Cidade> SelecionarEntidadeAsyncComInclude(Expression<Func<Cidade, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _cidadeRepository.SelecionarEntidadeAsyncComInclude(expression, cancellationToken);
        }

        public async Task<List<Cidade>> SelecionarTodosAsyncComInclude(CancellationToken cancellationToken = default)
        {
            return await _cidadeRepository.SelecionarTodosAsyncComInclude(cancellationToken);
        }

        public async Task<PagedResult<Cidade>> SelecionarPaginadoAsync(string nome, int page, int pageSize, Guid[] estadosIgnorados = null, CancellationToken cancellationToken = default)
        {
            bool temEstadosIgnorados = estadosIgnorados != null && estadosIgnorados.Length > 0;

            Expression<Func<Cidade, bool>> filtro = c => (string.IsNullOrWhiteSpace(nome) || c.Nome.ToLower().Contains(nome.ToLower())) 
                                                      && (!temEstadosIgnorados || !estadosIgnorados.Contains(c.EstadoId));

            return await _cidadeRepository.SelecionarPaginadoAsync(filtro, page, pageSize, c => c.Nome, cancellationToken, NomeEntidadesParaIncludePaginacao.Estado);
        }
    }
}
