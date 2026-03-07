using Fretefy.Test.Domain.Resources.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao: Entity
    {
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        public virtual List<RelacionamentoRegiaoCidadeUF> RelacionamentosRegiaoCidadesUF { get; private set; }
        
        public Regiao() { }

        public Regiao(string nome, List<Guid> cidadesIds, List<Guid> estadosIds)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Ativo = true;

            Validar(cidadesIds, estadosIds);

            if (!Invalido)
            {
                RelacionamentosRegiaoCidadesUF = new List<RelacionamentoRegiaoCidadeUF>();

                if (cidadesIds != null)
                {
                    RelacionamentosRegiaoCidadesUF.AddRange(cidadesIds.Select(cId => new RelacionamentoRegiaoCidadeUF(Id, cId, null)));
                }

                if (estadosIds != null)
                {
                    RelacionamentosRegiaoCidadesUF.AddRange(estadosIds.Select(eId => new RelacionamentoRegiaoCidadeUF(Id, null, eId)));
                }
            }
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        private void Validar(List<Guid> cidadesIds, List<Guid> estadosIds)
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                AdicionarMensagem(ValidacaoRegiaoResource.RegiaoNomeObrigatorio);
            }

            if ((cidadesIds == null || !cidadesIds.Any()) && (estadosIds == null || !estadosIds.Any()))
            {
                AdicionarMensagem(ValidacaoRegiaoResource.RegiaoSemCidadeUF);
            }
        }

        public void Atualizar(string nome, List<Guid> cidadesIds, List<Guid> estadosIds)
        {
            Nome = nome;
            Validar(cidadesIds, estadosIds);

            if (Invalido) 
            { 
                return; 
            }

            RelacionamentosRegiaoCidadesUF.Clear();

            if (cidadesIds != null)
            {
                RelacionamentosRegiaoCidadesUF.AddRange(cidadesIds.Select(cId => new RelacionamentoRegiaoCidadeUF(Id, cId, null)));
            }

            if (estadosIds != null)
            {
                RelacionamentosRegiaoCidadesUF.AddRange(estadosIds.Select(eId => new RelacionamentoRegiaoCidadeUF(Id, null, eId)));
            }
        }
    }
}
