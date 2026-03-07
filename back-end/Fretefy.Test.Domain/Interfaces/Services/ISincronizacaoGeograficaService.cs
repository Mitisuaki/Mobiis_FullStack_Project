using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface ISincronizacaoGeograficaService
    {
        Task SincronizarCidadesEUFIBGEAsync(CancellationToken cancellationToken);
    }
}
