namespace Solution.DTO.Models.Reports;

public class CellModel
{
	public int Index { get; set; }
	public string Value { get; set; }

}
public class RowData
{
	public List<CellModel> row = [];

}