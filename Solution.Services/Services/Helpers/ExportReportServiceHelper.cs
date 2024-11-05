using Solution.DTO.Models.Reports;
using Solution.Services.IServices.Helpers;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Hosting;

namespace Solution.Services.Services.Helpers;

public class ExportReportServiceHelper(IHostingEnvironment environment) : IExportReportServiceHelper
{
	private readonly IHostingEnvironment _environment = environment;

	public string NewGenerateReport(List<RowData> Rows, int[] Indexes, string TemplateName, int ColumnCount)
	{
		string path = Path.Combine(_environment.ContentRootPath, "Templates");
		var FileTemplate = Directory.GetFiles(path);

		string folderPath = CreateContainerFolder("Temp");
		string filePath = folderPath + "\\Report_" + DateTime.Now.Ticks + ".xlsx";
		foreach (var filename in FileTemplate)
		{
			string file = filename.ToString();

			if (!File.Exists(filePath) && file.Contains(TemplateName))
			{
				File.Copy(file, filePath);
			}
		}

		using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, true))
		{
			WorkbookPart workbookPart = doc.WorkbookPart;
			Sheet sheet = workbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
			WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
			SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

			InsertDataToSheetNewReport(Rows, ColumnCount, sheetData);

			//worksheetPart.Worksheet.Append(data);

			workbookPart.Workbook.Save();
			doc.Save();
		}
		Byte[] bytes = File.ReadAllBytes(filePath);
		String b64Str = Convert.ToBase64String(bytes);

		File.Delete(filePath);
		return b64Str;
	}

	private void InsertDataToSheetNewReport(List<RowData> Rows, int ColumnCount, SheetData worksheet)
	{
		var row_date = worksheet.Elements<Row>().Where(r => r.RowIndex is not null && r.RowIndex == 2).FirstOrDefault();
		var cell = row_date.Elements<Cell>().Where(c => c.CellReference.HasValue).First();
		cell.CellValue = new CellValue(DateTime.Now.Date);
		cell.DataType = new EnumValue<CellValues>(CellValues.Date);

		string value;
		int startRow = 3;
		for (int r = 0; r < Rows.Count; r++)
		{
			Row row = new Row();
			for (int c = 0; c <= ColumnCount; c++)
			{
				value = Rows[r].row.FirstOrDefault(x => x.Index == (c + 1))?.Value;
				row.InsertAt<Cell>(new Cell()
				{
					DataType = CellValues.InlineString,
					InlineString = new InlineString() { Text = new Text(value) },
				}, c);

			}
			worksheet.InsertAt(row, startRow++);
		}
	}

	private string CreateContainerFolder(string folderName)
	{
		//create folder to include files 
		var containerFolder = Path.Combine(_environment.ContentRootPath, folderName);
		bool IsExists = Directory.Exists(containerFolder);
		if (!IsExists)
			Directory.CreateDirectory(containerFolder);
		return containerFolder;
	}
}