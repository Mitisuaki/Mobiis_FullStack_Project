using ClosedXML.Excel;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Enums;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using Fretefy.Test.Domain.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Fretefy.Test.Domain.Constantes.DominioConstants;
using static Fretefy.Test.Infra.Constantes.InfraConstants;

namespace Fretefy.Test.Infra.Services
{
    public class ExportacaoService : IExportacaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;

        public ExportacaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public async Task<(Stream, string, string)> ExportarRegioesAsync(TipoExportacaoEnum formato, bool? ativo, CancellationToken cancellationToken)
        {
            List<Regiao> regioes = await _regiaoRepository.SelecionarTodosAsyncComInclude(cancellationToken);

            List<Regiao> regioesFiltradas = ativo.HasValue
                                            ? regioes.Where(r => r.Ativo == ativo.Value).ToList()
                                            : regioes.ToList();

            string contentType = formato == TipoExportacaoEnum.CSV
                                            ? Exportacao.CSV
                                            : Exportacao.Excel;

            string fileName = formato == TipoExportacaoEnum.CSV
                                         ? Exportacao.ExportacaoRegioesCSV
                                         : Exportacao.ExportacaoRegioesExcel;

            return formato switch
            {
                TipoExportacaoEnum.CSV => (GerarCSVStream(regioesFiltradas), contentType, fileName),
                TipoExportacaoEnum.Excel => (GerarExcelStream(regioesFiltradas), contentType, fileName),
                _ => throw new ArgumentOutOfRangeException(nameof(formato), ExportacaoServiceResource.FormatoExportacaoInvalido)
            };
        }

        private Stream GerarCSVStream(List<Regiao> regioes)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream, Encoding.UTF8, ExportacaoConfig.BufferSize, leaveOpen: true);

            writer.WriteLine($"{ExportacaoConfig.Regiao};{ExportacaoConfig.CidadeUF};{ExportacaoConfig.Status}");

            foreach (Regiao regiao in regioes)
            {
                string status = regiao.Ativo ? Status.Ativo : Status.Inativo;
                string cidadesUFs = string.Join(", ", regiao.RelacionamentosRegiaoCidadesUF.Select(rrcu => rrcu.CidadeId.HasValue ? rrcu.Cidade.Nome : rrcu.Estado.Nome));

                writer.WriteLine($"{regiao.Nome};\"{cidadesUFs}\";{status}");
            }

            writer.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }

        private Stream GerarExcelStream(IEnumerable<Regiao> regioes)
        {
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add(ExportacaoConfig.NomeTabela);

            worksheet.Cell(1, 1).Value = ExportacaoConfig.Regiao;
            worksheet.Cell(1, 2).Value = ExportacaoConfig.CidadeUF;
            worksheet.Cell(1, 3).Value = ExportacaoConfig.Status;
            worksheet.Range("A1:C1").Style.Font.Bold = true;

            int row = 2;
            foreach (Regiao regiao in regioes)
            {
                worksheet.Cell(row, 1).Value = regiao.Nome.ToString();
                worksheet.Cell(row, 2).Value = string.Join(", ", regiao.RelacionamentosRegiaoCidadesUF.Select(rrcu => rrcu.CidadeId.HasValue ? rrcu.Cidade.Nome : rrcu.Estado.Nome));
                worksheet.Cell(row, 3).Value = regiao.Ativo ? Status.Ativo : Status.Inativo;
                row++;
            }

            worksheet.Column(1).AdjustToContents();
            worksheet.Column(3).AdjustToContents();

            worksheet.Column(2).Width = 60;
            worksheet.Column(2).Style.Alignment.WrapText = true;

            MemoryStream memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }

}
