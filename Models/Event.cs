using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Portfolio_API.Models;

public class Event
{
    public int EventId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    [JsonIgnore]
    public Item Item { get; set; }
}