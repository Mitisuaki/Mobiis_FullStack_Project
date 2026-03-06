namespace Fretefy.Test.Infra.Constantes
{
    public static class InfraConstants
    {
        public static class ConnectionStrings
        {
            public const string DefaultConnection = "Default";
        }

        public static class Swagger
        {
            public const string Version = "V1";
            public const string Title = "Mobiis FullStack Project";
            public const string URL = "/swagger/v1/swagger.json";
            public const string Nome = Title + " " + Version;
        }

        public static class APIsExternas
        {
            public const string IBGEMunicipios = "https://servicodados.ibge.gov.br/api/v1/localidades/municipios";
        }
    }
}
