using System;
using System.Collections.Generic;

namespace Fretefy.Test.Application.Models.Regiao
{
    public class RegiaoDetalheDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public List<Guid> CidadesIds { get; set; } = new List<Guid>();
        public List<Guid> EstadosIds { get; set; } = new List<Guid>();
    }
}
