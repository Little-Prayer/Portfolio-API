namespace Portfolio_API.Models;
public class Category
{
    public int CategoryId { get; set; }
    public String Name { get; set; } = null!;
    public List<Item> Items { get; } = new();
    //test
}