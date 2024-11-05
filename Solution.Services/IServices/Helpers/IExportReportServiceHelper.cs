using Solution.DTO.Models.Reports;

namespace Solution.Services.IServices.Helpers;

public interface IExportReportServiceHelper
{
	string NewGenerateReport(List<RowData> Rows, int[] Indexes, string TemplateName, int ColumnCount);
}