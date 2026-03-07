using System;
using System.Collections.Generic;

namespace Fretefy.Test.Application.Models.Regiao
{
    public class RegiaoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public List<string> Cidades { get; set; } = new List<string>();
        public List<string> Estados { get; set; } = new List<string>();
    }
}
