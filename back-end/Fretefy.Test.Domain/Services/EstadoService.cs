using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public class EstadoService : BaseService<Estado>, IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(IEstadoRepository estadoRepository) : base(estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task<List<Estado>> SelecionarListaAsync(string uf, CancellationToken cancellationToken)
        {
            List<Estado> estados;

            if (string.IsNullOrWhiteSpace(uf))
            {
                estados = await _estadoRepository.SelecionarListaAsync(e => true, cancellationToken);
            }
            else
            {
                estados = await _estadoRepository.SelecionarListaAsync(e => e.Nome.Contains(uf) || e.Sigla.Contains(uf), cancellationToken);
            }

            return estados.OrderBy(e => e.Nome).ToList();
        }
    }
}
