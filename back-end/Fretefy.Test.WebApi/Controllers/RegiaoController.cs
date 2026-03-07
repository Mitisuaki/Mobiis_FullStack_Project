using Fretefy.Test.Application.Interfaces;
using Fretefy.Test.Application.Models.Regiao;
using Fretefy.Test.Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoAppService _regiaoAppService;

        public RegiaoController(IRegiaoAppService regiaoAppService)
        {
            _regiaoAppService = regiaoAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid regiaoId, CancellationToken cancellationToken)
        {
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
            await _regiaoAppService.SalvarAsync(regiao, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return BadRequest(new { erros = _regiaoAppService.Mensagens });
            }

            return Ok(new { mensagem = string.Format(MensagensRegiaoControllerResource.RegiaoCriadaComSucesso, regiao.Nome) });
        }

        [HttpPut("{regiaoId}")]
        public async Task<IActionResult> Put(Guid regiaoId, [FromBody] RegiaoInputModel regiao, CancellationToken cancellationToken)
        {
            await _regiaoAppService.AtualizarAsync(regiaoId, regiao, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return BadRequest(new { erros = _regiaoAppService.Mensagens });
            }

            return Ok(new { mensagem = string.Format(MensagensRegiaoControllerResource.RegiaoAtualizadaComSucesso, regiao.Nome) });
        }

        [HttpPut("{id}/ativar")]
        public async Task<IActionResult> Ativar(Guid regiaoId, CancellationToken cancellationToken)
        {
            await _regiaoAppService.AtivarAsync(regiaoId, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return BadRequest(new { erros = _regiaoAppService.Mensagens });
            }

            return Ok(new { mensagem = MensagensRegiaoControllerResource.RegiaoAtivadaComSucesso });
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> Inativar(Guid regiaoId, CancellationToken cancellationToken)
        {
            await _regiaoAppService.InativarAsync(regiaoId, cancellationToken);

            if (_regiaoAppService.Invalido)
            {
                return BadRequest(new { erros = _regiaoAppService.Mensagens });
            }

            return Ok(new { mensagem = MensagensRegiaoControllerResource.RegiaoInativadaComSucesso });
        }
    }
}
