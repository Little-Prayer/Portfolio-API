using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.Models;

public class Event
{
    public int EventId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public Item? Item { get; set; }
    public string? Memo { get; set; }
}