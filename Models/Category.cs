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

        public override int GetHashCode()
    {
        return Name!.GetHashCode() ^ CategoryId!.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if(obj is null) return false;
        Category? cat = obj as Category;
        if(cat == null)return false;
        return Name == cat.Name && CategoryId == cat.CategoryId;
    }

}