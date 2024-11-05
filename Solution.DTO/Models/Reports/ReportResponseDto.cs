namespace Solution.DTO.Models.Reports;

public class ReportResponseDto
{
	public Column[] Columns { get; set; }
	public Rows[] Rows { get; set; }

}

public class Column
{
	public int Id { get; set; }
	public string Type { get; set; }
	public string Value { get; set; }
	public string Label { get; set; }
	public int Order { get; set; }

}

public class Rows
{
	public string Id { get; set; }
	public Data[] Row { get; set; }
}

public class Data
{
	public int Id { get; set; }
	public int Column { get; set; }
	public string Value { get; set; }


}
