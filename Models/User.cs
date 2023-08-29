using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.Models;

public class User
{
    public int UserId{ get; set; }

    [Required]
    public string? UserEmail{ get; set; }

    public string? UserName{ get; set; }

    public ISet<Item>? Items{ get; set; }
    public ISet<Swap>? Swaps{ get; set; }
    public ISet<Category>? Categories{ get; set; }
}