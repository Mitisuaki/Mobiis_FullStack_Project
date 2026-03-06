using Fretefy.Test.Domain.Interfaces.Entities;
using System;

namespace Fretefy.Test.Domain.Entities
{
    public class Cidade : IEntity
    {        
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string UF { get; set; }

        public Cidade()
        {

        }

        public Cidade(string nome, string uf)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            UF = uf;
        }
    }
}
