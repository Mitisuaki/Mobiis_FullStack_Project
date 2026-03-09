using Fretefy.Test.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IEstadoService : IBaseService<Estado>
    {
        Task<List<Estado>> SelecionarListaAsync(string uf, CancellationToken cancellationToken = default);
    }
}
