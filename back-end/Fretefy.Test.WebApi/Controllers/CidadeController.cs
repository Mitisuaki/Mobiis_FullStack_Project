using Fretefy.Test.Application.Interfaces;
using Fretefy.Test.Application.Models.Cidade;
using Fretefy.Test.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/cidade")]
    [ApiController]
    public class CidadeController : Controller
    {
        private readonly ICidadeAppService _cidadeAppService;

        public CidadeController(ICidadeAppService cidadeAppService)
        {
            _cidadeAppService = cidadeAppService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string nome, 
                                             [FromQuery] Guid? estadoId,
                                             [FromQuery] int page = 1,
                                             [FromQuery] int pageSize = 50,
                                             CancellationToken cancellationToken = default)
        {
            if (estadoId.HasValue)
            {
                var cidadesPorEstado = await _cidadeAppService.ObterPorIdAsync(estadoId.Value, cancellationToken);
                return Ok(cidadesPorEstado);
            }

            PagedResult<CidadeDTO> cidadesPaginadas = await _cidadeAppService.ObterTodasPaginadoAsync(nome, page, pageSize, cancellationToken);
            return Ok(cidadesPaginadas);
        }
    }
}
