using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Models;

[Index(nameof(Name),IsUnique=true)]
public class Category
{
    public int CategoryId { get; set; }
    [Required]
    public String? Name { get; set; } 
    [JsonIgnore]
    public List<Item> Items { get; } = new();
    //test
}