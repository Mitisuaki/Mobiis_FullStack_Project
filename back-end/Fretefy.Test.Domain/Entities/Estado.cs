using Fretefy.Test.Domain.Resources.Validacao;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities
{
    public class Estado : Entity
    {
        public string Nome { get; private set; }
        public string Sigla { get; private set; }

        public virtual List<Cidade> Cidades { get; private set; }

        public Estado() { }

        public Estado(string nome, string sigla)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Sigla = sigla;

            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                AdicionarMensagem(ValidacaoEstadoResource.EstadoNomeObrigatorio);
            }

            if (string.IsNullOrWhiteSpace(Sigla) || Sigla.Length != 2)
            {
                AdicionarMensagem(ValidacaoEstadoResource.EstadoSiglaInvalida);
            }
        }
    }
}
