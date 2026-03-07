using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<PagedResult<Cidade>> SelecionarPaginadoAsync(string nome, int page, int pageSize, CancellationToken cancellationToken)
        {
            Expression<Func<Cidade, bool>> filtro = c => true;

            if (!string.IsNullOrWhiteSpace(nome))
            {
                filtro = c => c.Nome.Contains(nome);
            }

            return await _cidadeRepository.SelecionarPaginadoAsync(filtro, page, pageSize, c => c.Nome, cancellationToken, "Estado");
        }
    }
}
