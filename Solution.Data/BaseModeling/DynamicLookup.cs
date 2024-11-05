using Solution.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Solution.Data.BaseModeling;

public class DynamicLookup : ILookupEntity
{
	public int Id { get; set; }
	[MaxLength(50), Required]
	public string Name { get; set; }
	public string Description { get; set; }
	public bool IsDeleted { get; set; }
}
