using Fretefy.Test.Application.Models.Cidade;
using Fretefy.Test.Application.Models.Estado;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Application.Models.Regiao
{
    public class RegiaoDetalheDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public List<CidadeDTO> Cidades { get; set; } = new List<CidadeDTO>();
        public List<EstadoDTO> Estados { get; set; } = new List<EstadoDTO>();
    }
}
