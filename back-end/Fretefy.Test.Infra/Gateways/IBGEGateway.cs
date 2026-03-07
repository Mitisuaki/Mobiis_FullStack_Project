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

        public async Task<List<MunicipioIBGEDTO>> ObterCidadesComUFAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage resposta = await _httpClient.GetAsync(IBGEMunicipios, cancellationToken);
            resposta.EnsureSuccessStatusCode();

            using Stream stream = await resposta.Content.ReadAsStreamAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<IBGEMunicipioDTO> municipiosIBGE = await JsonSerializer.DeserializeAsync<List<IBGEMunicipioDTO>>(stream, options, cancellationToken);

            return municipiosIBGE?.Select(m => new MunicipioIBGEDTO
            {
                CidadeNome = m.Nome,
                EstadoNome = m.Microrregiao.Mesorregiao.UF.Nome,
                EstadoSigla = m.Microrregiao.Mesorregiao.UF.Sigla
            }).ToList() ?? new List<MunicipioIBGEDTO>();
        }
    }
}
