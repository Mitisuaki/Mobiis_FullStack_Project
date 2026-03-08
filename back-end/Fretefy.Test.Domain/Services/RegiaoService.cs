using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using Fretefy.Test.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public class RegiaoService : BaseService<Regiao>, IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IEstadoRepository _estadoRepository;

        public RegiaoService(IRegiaoRepository regiaoRepository,
                             ICidadeRepository cidadeRepository,
                             IEstadoRepository estadoRepository) : base(regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
            _cidadeRepository = cidadeRepository;
            _estadoRepository = estadoRepository;
        }

        public async Task<Regiao> SelecionarEntidadeAsyncComInclude(Expression<Func<Regiao, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _regiaoRepository.SelecionarEntidadeAsyncComInclude(expression, cancellationToken);
        }

        public async Task<List<Regiao>> SelecionarTodosAsyncComInclude(CancellationToken cancellationToken = default)
        {
            return await _regiaoRepository.SelecionarTodosAsyncComInclude(cancellationToken);
        }
        public async Task SalvarRegiaoAsync(string nome, List<Guid> cidadesIds, List<Guid> estadosIds, CancellationToken cancellationToken = default)
        {
            (cidadesIds, estadosIds) = await ValidarCidadesEEstados(cidadesIds, estadosIds);

            if (Invalido)
            {
                return;
            }

            Regiao regiao = new Regiao(nome, cidadesIds, estadosIds);

            if (regiao.Invalido)
            {
                AdicionarRangeMensagens(regiao.Mensagens);
                return;
            }

            await _regiaoRepository.AdicionarAsync(regiao, cancellationToken);
            await _regiaoRepository.SalvarAsync(cancellationToken);
        }

        private async Task<(List<Guid> cidadesIds, List<Guid> estadosIds)> ValidarCidadesEEstados(List<Guid> cidadesIds, List<Guid> estadosIds, CancellationToken cancellationToken = default)
        {
            cidadesIds ??= new List<Guid>();
            estadosIds ??= new List<Guid>();

            if (!cidadesIds.Any() && !estadosIds.Any())
            {
                AdicionarMensagem(MensagensRegiaoServiceResource.RegiaoSemCidadeUF);

                return (cidadesIds, estadosIds);
            }

            List<Cidade> cidadesExistentes = await _cidadeRepository.SelecionarListaAsync(c => cidadesIds.Contains(c.Id), cancellationToken);
            List<Guid> cidadesInexistentes = cidadesIds.Except(cidadesExistentes.Select(c => c.Id)).ToList();

            foreach (Guid id in cidadesInexistentes)
            {
                AdicionarMensagem(string.Format(MensagensRegiaoServiceResource.RegiaoServiceCidadeNaoEncontrada, id));
            }

            List<Estado> estadosExistentes = await _estadoRepository.SelecionarListaAsync(e => estadosIds.Contains(e.Id), cancellationToken);
            List<Guid> estadosInexistentes = estadosIds.Except(estadosExistentes.Select(e => e.Id)).ToList();

            foreach (Guid id in estadosInexistentes)
            {
                AdicionarMensagem(string.Format(MensagensRegiaoServiceResource.RegiaoServiceEstadoNaoEncontado, id));
            }

            List<Guid> cidadesDuplicadas = cidadesIds.GroupBy(id => id).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            foreach (Guid id in cidadesDuplicadas)
            {
                Cidade cidade = cidadesExistentes.FirstOrDefault(c => c.Id == id);
                AdicionarMensagem(string.Format(MensagensRegiaoServiceResource.RegiaoServiceCidadeDuplicada, cidade.Nome));
            }

            List<Guid> estadosDuplicados = estadosIds.GroupBy(id => id).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            foreach (Guid id in estadosDuplicados)
            {
                Estado estado = estadosExistentes.FirstOrDefault(e => e.Id == id);
                AdicionarMensagem(string.Format(MensagensRegiaoServiceResource.RegiaoServiceEstadoDuplicado, estado.Sigla));
            }

            List<Cidade> cidadesComEstadosInclusos = cidadesExistentes.Where(c => estadosExistentes.Any(e => e.Id == c.EstadoId)).ToList();
            foreach (Cidade cidade in cidadesComEstadosInclusos)
            {
                Estado estado = estadosExistentes.FirstOrDefault(e => e.Id == cidade.EstadoId);
                AdicionarMensagem(string.Format(MensagensRegiaoServiceResource.RegiaoServiceCidadeComEstadoIncluso, cidade.Nome, estado.Sigla));
            }

            return (cidadesIds, estadosIds);
        }
        public async Task AtivarAsync(Guid id, CancellationToken cancellationToken)
        {
            Regiao regiao = await _regiaoRepository.SelecionarEntidadeAsync(r => r.Id == id, cancellationToken);

            if (regiao == null)
            {
                AdicionarMensagem(MensagensRegiaoServiceResource.RegiaoServiceRegiaoNaoEncontrada);
                return;
            }

            regiao.Ativar();
            await _regiaoRepository.SalvarAsync(cancellationToken);
        }
        public async Task InativarAsync(Guid regiaoId, CancellationToken cancellationToken = default)
        {
            Regiao regiao = await _regiaoRepository.SelecionarEntidadeAsync(r => r.Id == regiaoId, cancellationToken);

            if (regiao == null)
            {
                AdicionarMensagem(MensagensRegiaoServiceResource.RegiaoServiceRegiaoNaoEncontrada);
                return;
            }

            regiao.Inativar();
            await _regiaoRepository.SalvarAsync(cancellationToken);
        }

        public async Task AtualizarAsync(Guid regiaoId, string nome, List<Guid> cidadesIds, List<Guid> estadosIds, CancellationToken cancellationToken = default)
        {
            
            Regiao regiao = await _regiaoRepository.SelecionarEntidadeAsync(r => r.Id == regiaoId, cancellationToken);

            if (regiao == null)
            {
                AdicionarMensagem(MensagensRegiaoServiceResource.RegiaoServiceRegiaoNaoEncontrada);
                return;
            }

            (cidadesIds, estadosIds) = await ValidarCidadesEEstados(cidadesIds, estadosIds, cancellationToken);

            if (Invalido) 
            { 
                return; 
            }

            regiao.Atualizar(nome, cidadesIds, estadosIds);

            if (regiao.Invalido)
            {
                AdicionarRangeMensagens(regiao.Mensagens);
                return;
            }

            await _regiaoRepository.SalvarAsync(cancellationToken);
        }
    }
}
