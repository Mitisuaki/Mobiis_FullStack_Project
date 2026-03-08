using Fretefy.Test.Domain.Enums;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IExportacaoService
    {
        Task<(Stream, string, string)> ExportarRegioesAsync(TipoExportacaoEnum formato, bool? ativo, CancellationToken cancellationToken);
    }
}
