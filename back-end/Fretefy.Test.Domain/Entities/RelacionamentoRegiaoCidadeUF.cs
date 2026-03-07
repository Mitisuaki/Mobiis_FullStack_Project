using System;

namespace Fretefy.Test.Domain.Entities
{
    public class RelacionamentoRegiaoCidadeUF : Entity
    {
        public Guid RegiaoId { get; private set; }
        public Guid? CidadeId { get; private set; }
        public Guid? EstadoId { get; private set; }


        public virtual Regiao Regiao { get; private set; }
        public virtual Cidade Cidade { get; private set; }
        public virtual Estado Estado { get; private set; }

        public RelacionamentoRegiaoCidadeUF() { }

        public RelacionamentoRegiaoCidadeUF(Guid regiaoId, Guid? cidadeId, Guid? estadoId)
        {
            Id = Guid.NewGuid();
            RegiaoId = regiaoId;
            CidadeId = cidadeId;
            EstadoId = estadoId;
        }
    }
}
