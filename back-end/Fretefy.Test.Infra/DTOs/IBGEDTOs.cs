using System.Text.Json.Serialization;

namespace Fretefy.Test.Infra.DTOs
{
    public class IBGEMunicipioDTO
    {
        [JsonPropertyName("Nome")]
        public string Nome { get; set; }

        [JsonPropertyName("Microrregiao")]
        public IBGEMicrorregiaoDTO Microrregiao { get; set; }
    }

    public class IBGEMicrorregiaoDTO
    {
        [JsonPropertyName("Mesorregiao")]
        public IBGEMesorregiaoDTO Mesorregiao { get; set; }
    }

    public class IBGEMesorregiaoDTO
    {
        [JsonPropertyName("UF")]
        public IBGEUfDTO UF { get; set; }
    }

    public class IBGEUfDTO
    {
        [JsonPropertyName("Sigla")]
        public string Sigla { get; set; }
    }
}
