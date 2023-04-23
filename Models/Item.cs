namespace Portfolio_API.Models;
public class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int SwapFrequency { get; set; }
    public List<Category> Categories { get; } = new();
    public List<Event> Events { get; } = new();
}