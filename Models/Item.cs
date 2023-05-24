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
    public List<Category>? Categories { get; }
    public List<Event>? Events { get; }
}