using System.Text.Json.Serialization;

namespace Fretefy.Test.Infra.DTOs
{
    public class IBGEMunicipioResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }
}
