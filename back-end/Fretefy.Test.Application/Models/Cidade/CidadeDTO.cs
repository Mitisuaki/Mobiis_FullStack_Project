using Fretefy.Test.Domain.Entities;
using System;

namespace Fretefy.Test.Application.Models.Cidade
{
    public class CidadeDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid EstadoId { get; set; }
        public string Estado { get; set; }
    }
}
