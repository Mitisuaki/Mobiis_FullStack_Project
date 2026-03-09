using Fretefy.Test.Application.Models.Estado;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Application.Interfaces
{
    public interface IEstadoAppService
    {
        Task<List<EstadoDTO>> SelecionarListaAsync(string uf, CancellationToken cancellationToken = default);
    }
}
