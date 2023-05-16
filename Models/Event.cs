using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.Models;

public class Event
{
    public int EventId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public Item Item { get; set; }=default!;
}