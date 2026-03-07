using Fretefy.Test.Domain.Resources.Validacao;
using System;

namespace Fretefy.Test.Domain.Entities
{
    public class Cidade : Entity
    {        
        public string Nome { get; private set; }
        public Guid EstadoId { get; private set; }

        public virtual Estado Estado { get; private set; }

        public Cidade() { }

        public Cidade(string nome, Guid estadoId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            EstadoId = estadoId;

            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                AdicionarMensagem(ValidacaoCidadeResource.CidadeNomeObrigatorio);
            }
            if (EstadoId == Guid.Empty)
            {
                AdicionarMensagem(ValidacaoCidadeResource.CidadeNaoVinculadaEstado);
            }
        }
    }
}
