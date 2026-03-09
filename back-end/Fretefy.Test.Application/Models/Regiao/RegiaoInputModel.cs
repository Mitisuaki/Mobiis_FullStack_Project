using System;
using System.Collections.Generic;

namespace Fretefy.Test.Application.Models.Regiao
{
    public class RegiaoInputModel
    {
        public string Nome { get; set; }
        public List<Guid> CidadesIds { get; set; } = new List<Guid>();
        public List<Guid> EstadosIds { get; set; } = new List<Guid>();
    }
}
