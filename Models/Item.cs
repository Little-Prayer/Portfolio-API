using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Models;

[Index(nameof(Name),IsUnique=true)]
public class Item
{
    public int ItemId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }
    public int? SwapFrequency { get; set; }
    public List<Category> Categories { get; } = new();
    public List<Event> Events { get; } = new();
}