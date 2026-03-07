using Fretefy.Test.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/estado")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoAppService _estadoAppService;

        public EstadoController(IEstadoAppService estadoAppService)
        {
            _estadoAppService = estadoAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string busca, CancellationToken cancellationToken)
        {
            var estados = await _estadoAppService.SelecionarListaAsync(busca, cancellationToken);
            return Ok(estados);
        }
    }
}
