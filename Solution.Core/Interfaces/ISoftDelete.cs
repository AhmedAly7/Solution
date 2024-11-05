namespace Solution.Core.Interfaces;

public interface ISoftDelete
{
	int? DeletedBy { get; set; }
	DateTime? DeletedAt { get; set; }
	bool IsDeleted { get; set; }
}