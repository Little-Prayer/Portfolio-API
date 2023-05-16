using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Models;

[Index(nameof(Name),IsUnique=true)]
public class Category
{
    public int CategoryId { get; set; }
    [Required]
    public String? Name { get; set; } 
    
    public List<Item> Items { get; } = new();

}