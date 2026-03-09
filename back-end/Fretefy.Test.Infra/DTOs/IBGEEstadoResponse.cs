using System.Text.Json.Serialization;

namespace Fretefy.Test.Infra.DTOs
{
    public class IBGEEstadoResponse
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Sigla")]
        public string Sigla { get; set; }

        [JsonPropertyName("Nome")]
        public string Nome { get; set; }
    }
}
