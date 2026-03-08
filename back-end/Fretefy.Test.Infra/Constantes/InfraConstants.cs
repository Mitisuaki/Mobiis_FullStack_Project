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
            public const string Version = "v1";
            public const string Title = "Mobiis FullStack Project";
            public const string URL = "/swagger/v1/swagger.json";
            public const string Nome = Title + " " + Version;
        }

        public static class APIsExternas
        {
            public const string IBGEEstados = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
            public const string IBGEMunicipios = "https://servicodados.ibge.gov.br/api/v1/localidades/municipios";
        }

        public static class Exportacao
        {
            public const string CSV = "text/csv";
            public const string Excel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";        
            public const string ExportacaoRegioesCSV = "regioes_exportacao.csv";
            public const string ExportacaoRegioesExcel = "regioes_exportacao.xlsx";
        }
    }
}
