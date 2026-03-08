using Fretefy.Test.Domain.DTOs;
using Fretefy.Test.Domain.Interfaces.Gateways;
using Fretefy.Test.Infra.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static Fretefy.Test.Infra.Constantes.InfraConstants.APIsExternas;

namespace Fretefy.Test.Infra.Gateways
{
    public class IBGEGateway : IIBGEGateway
    {
        private readonly HttpClient _httpClient;

        public IBGEGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EstadoIBGEDTO>> ObterEstadosAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage resposta = await _httpClient.GetAsync(IBGEEstados, cancellationToken);
            resposta.EnsureSuccessStatusCode();

            using Stream stream = await resposta.Content.ReadAsStreamAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<IBGEEstadoResponse> estadosIBGE = await JsonSerializer.DeserializeAsync<List<IBGEEstadoResponse>>(stream, options, cancellationToken);

            return estadosIBGE?.Select(e => new EstadoIBGEDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                Sigla = e.Sigla
            }).ToList() ?? new List<EstadoIBGEDTO>();
        }

        public async Task<List<MunicipioIBGEDTO>> ObterCidadesAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage resposta = await _httpClient.GetAsync(IBGEMunicipios, cancellationToken);
            resposta.EnsureSuccessStatusCode();

            using Stream stream = await resposta.Content.ReadAsStreamAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<IBGEMunicipioResponse> municipiosIBGE = await JsonSerializer.DeserializeAsync<List<IBGEMunicipioResponse>>(stream, options, cancellationToken);

            return municipiosIBGE?.Select(m => new MunicipioIBGEDTO
            {
                Id = m.Id,
                Nome = m.Nome,
            }).ToList() ?? new List<MunicipioIBGEDTO>();
        }
    }
}
