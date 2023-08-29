using Microsoft.EntityFrameworkCore;
using Portfolio_API.Models;

namespace Portfolio_API.Data;

public class ItemContext : DbContext
{
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Swap> Swaps => Set<Swap>();

    public DbSet<User> Users => Set <User>();

    public ItemContext(DbContextOptions<ItemContext> options)
     : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Item>()
                    .HasMany(i=>i.Categories)
                    .WithMany(c=>c.Items);
    }
}
