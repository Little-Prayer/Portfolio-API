using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Models;

[Index(nameof(Name),IsUnique=true)]
public class Item
{
    public int? ItemId { get; set; }
    public string? Name { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Price { get; set; }
    public long? Ticks { get; set; }
    public User? User{ get; set; }
    public ISet<Category>? Categories { get; set;}
    public IList<Swap>? Swaps { get; set;}
}