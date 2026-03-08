using Fretefy.Test.Application.Interfaces;
using Fretefy.Test.Application.Models.Estado;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fretefy.Test.Application.Services
{
    public class EstadoAppService : IEstadoAppService
    {
        private readonly IEstadoService _estadoService;

        public EstadoAppService(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        public async Task<List<EstadoDTO>> SelecionarListaAsync(string uf, CancellationToken cancellationToken = default)
        {
            List<Estado> estados = await _estadoService.SelecionarListaAsync(uf, cancellationToken);

            return estados.Select(e => new EstadoDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                Sigla = e.Sigla
            }).ToList();
        }
    }
}
