namespace Solution.Core.Interfaces;

public interface IAuditable
{
	public int? CreatedBy { get; set; }
	public DateTime? CreationDate { get; set; }
	public int? LastUpdatedBy { get; set; }
	public DateTime? LastUpdateDate { get; set; }
}