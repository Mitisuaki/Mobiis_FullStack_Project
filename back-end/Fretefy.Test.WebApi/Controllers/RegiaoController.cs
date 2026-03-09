using Fretefy.Test.Application.Interfaces;
using Fretefy.Test.Application.Models.Regiao;
using Fretefy.Test.Domain.Enums;
using Fretefy.Test.Domain.Interfaces.Services;
using Fretefy.Test.Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoAppService _regiaoAppService;
        private readonly IExportacaoService _exportacaoService;

        public RegiaoController(IRegiaoAppService regiaoAppService,
                                IExportacaoService exportacaoService)
        {
            _regiaoAppService = regiaoAppService;
            _exportacaoService = exportacaoService;
        }

        [HttpGet("{regiaoId}")]
        public async Task<IActionResult> GetById(Guid regiaoId, CancellationToken cancellationToken)
        {
            if (regiaoId == Guid.Empty)
            {
                return BadRequest(new { Mensagens = MensagensRegiaoControllerResource.RegiaoIdInvalido});
            }

            RegiaoDetalheDTO regiao = await _regiaoAppService.ObterPorIdAsync(regiaoId, cancellationToken);

            if (regiao == null)
            {
                return NotFound();
            }

            return Ok(regiao);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<RegiaoDTO> regioes = await _regiaoAppService.ObterTodasAsync(cancellationToken);

            return Ok(regioes);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegiaoInputModel regiao, CancellationToken cancellationToken)
        {
            if (regiao == null)
            {
                return BadRequest(new { Mensagens = MensagensRegiaoControllerResource.RegiaoObrigatoria});
            }

            await _regiaoAppService.SalvarAsync(regiao, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return BadRequest(new { _regiaoAppService.Mensagens });
            }

            return Ok(new { Mensagens = string.Format(MensagensRegiaoControllerResource.RegiaoCriadaComSucesso, regiao.Nome) });
        }

        [HttpPut("{regiaoId}")]
        public async Task<IActionResult> Put(Guid regiaoId, [FromBody] RegiaoInputModel regiao, CancellationToken cancellationToken)
        {
            if (regiaoId == Guid.Empty)
            {
                return BadRequest(new { Mensagens = MensagensRegiaoControllerResource.RegiaoIdInvalido});
            }

            if (regiao == null)
            {
                return BadRequest(new { Mensagens = MensagensRegiaoControllerResource.RegiaoObrigatoria});
            }

            await _regiaoAppService.AtualizarAsync(regiaoId, regiao, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return BadRequest(new { _regiaoAppService.Mensagens });
            }

            return Ok(new { Mensagens = string.Format(MensagensRegiaoControllerResource.RegiaoAtualizadaComSucesso, regiao.Nome) });
        }

        [HttpPut("{regiaoId}/ativar")]
        public async Task<IActionResult> Ativar(Guid regiaoId, CancellationToken cancellationToken)
        {
            if (regiaoId == Guid.Empty)
            {
                return BadRequest(new { Mensagens = MensagensRegiaoControllerResource.RegiaoIdInvalido});
            }

            await _regiaoAppService.AtivarAsync(regiaoId, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return NotFound();
            }

            return Ok(new { Mensagens = MensagensRegiaoControllerResource.RegiaoAtivadaComSucesso });
        }

        [HttpPut("{regiaoId}/inativar")]
        public async Task<IActionResult> Inativar(Guid regiaoId, CancellationToken cancellationToken)
        {
            if (regiaoId == Guid.Empty)
            {
                return BadRequest(new { Mensagens = MensagensRegiaoControllerResource.RegiaoIdInvalido });
            }

            await _regiaoAppService.InativarAsync(regiaoId, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return NotFound();
            }

            return Ok(new { Mensagens = MensagensRegiaoControllerResource.RegiaoInativadaComSucesso });
        }

        [HttpDelete("{regiaoId}")]
        public async Task<IActionResult> Delete(Guid regiaoId, CancellationToken cancellationToken)
        {
            if (regiaoId == Guid.Empty)
            {
                return BadRequest(new { Mensagens = MensagensRegiaoControllerResource.RegiaoIdInvalido });
            }

            await _regiaoAppService.ExcluirEntidade(regiaoId, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return NotFound();
            }

            return Ok(new { Mensagens = MensagensRegiaoControllerResource.RegiaoExcluidaComSucesso });
        }

        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarRegioes([FromQuery] TipoExportacaoEnum formato, [FromQuery] bool? ativo, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(TipoExportacaoEnum), formato))
            {
                return BadRequest(new { Mensagens = MensagensRegiaoControllerResource.FormatoExportacaoInvalido});
            }

            (Stream stream, string contentType, string fileName) = await _exportacaoService.ExportarRegioesAsync(formato, ativo, cancellationToken);
            
            return File(stream, contentType, fileName);
        }
    }
}
