using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Solution.Core.Interfaces;

namespace Solution.Data.BaseModeling;

public class StaticLookup : ILookupEntity
{
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public int Id { get; set; }
	[MaxLength(50), Required]
	public string Name { get; set; }
	public string Description { get; set; }
	public bool IsDeleted { get; set; }
}