using Microsoft.EntityFrameworkCore;
using Portfolio_API.Models;

namespace Portfolio_API.Data;

public class ItemContext : DbContext
{
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Event> Events => Set<Event>();

    public ItemContext(DbContextOptions<ItemContext> options)
     : base(options)
    {
    }

}
