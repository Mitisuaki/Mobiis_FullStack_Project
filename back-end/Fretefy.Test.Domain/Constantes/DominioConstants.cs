namespace Fretefy.Test.Domain.Constantes
{
    public static class DominioConstants
    {
        public static class Status
        {
            public const string Ativo = "Ativo";
            public const string Inativo = "Inativo";
        }

        public static class ExportacaoConfig
        {
            public const string Regiao = "Região";
            public const string CidadeUF = "Cidade(s)/UF(s)";
            public const string Status = "Status";

            public const int BufferSize = 1024;

            public const string NomeTabela = "Regiões";
        }

        public static class NomeEntidadesParaIncludePaginacao
        {
            public const string Estado = "Estado";
        }
    }
}
