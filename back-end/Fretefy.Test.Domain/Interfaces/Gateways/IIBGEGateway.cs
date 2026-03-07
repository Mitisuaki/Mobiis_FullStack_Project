using Fretefy.Test.Domain.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Gateways
{
    public interface IIBGEGateway
    {
        Task<List<MunicipioIBGEDTO>> ObterCidadesComUFAsync(CancellationToken cancellationToken);
    }
}
