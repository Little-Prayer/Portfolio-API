using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.Models;

public class Swap
{
    public int SwapId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public Item? Item { get; set; }
    public User? User { get; set; }
    public string? Memo { get; set; }    
}