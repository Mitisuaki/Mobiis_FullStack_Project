using Fretefy.Test.Application.Interfaces;
using Fretefy.Test.Application.Models.Cidade;
using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Get([FromQuery] Guid cidadeId, CancellationToken cancellationToken = default)
        {
            if (cidadeId == Guid.Empty)
            {
                return BadRequest(MensagensCidadeControllerResource.CidadeIdInvalido);
            }

            CidadeDTO cidadesPorEstado = await _cidadeAppService.ObterPorIdAsync(cidadeId, cancellationToken);
            if (cidadesPorEstado != null)
            {
                return Ok(cidadesPorEstado);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginado([FromQuery] string nome, 
                                                     [FromQuery] int page = 1,
                                                     [FromQuery] int pageSize = 50,
                                                     CancellationToken cancellationToken = default)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(MensagensCidadeControllerResource.ConfigPaginacaoInvalido);
            }

            PagedResult<CidadeDTO> cidadesPaginadas = await _cidadeAppService.ObterTodasPaginadoAsync(nome, page, pageSize, cancellationToken);
            
            if (cidadesPaginadas != null && cidadesPaginadas.Items.Any())
            {
                return Ok(cidadesPaginadas);
            }

            return NotFound();
        }

        [HttpGet("todas")]
        public async Task<IActionResult> SelecionarTodasCidades(CancellationToken cancellationToken)
        {
            List<CidadeDTO> cidades = await _cidadeAppService.SelecionarTodas(cancellationToken);
            
            if (cidades != null && cidades.Any())
            {
                return Ok(cidades);
            }

            return NotFound();
        }
    }
}
