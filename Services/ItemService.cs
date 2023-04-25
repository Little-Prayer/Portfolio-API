using Portfolio_API.Data;
using Portfolio_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Services;

public class ItemService
{
    private readonly ItemContext _context;

    public ItemService(ItemContext context)
    {
        _context = context;
    }

    public IEnumerable<Item> GetAll()
    {
        return _context.Items
            .AsNoTracking()
            .ToList();
    }

    public Item? GetById(int id)
    {
        return _context.Items
            .Include(i => i.Categories)
            .Include(i => i.Events)
            .AsNoTracking()
            .SingleOrDefault(i => i.ItemId == id);
    }

    public Item Create(Item newItem)
    {
        _context.Items.Add(newItem);
        _context.SaveChanges();

        return newItem;
    }

    public void AddEvent(int itemId,int eventId)
    {
        var itemToUpdate = _context.Items.Find(itemId);
        var eventToAdd = _context.Events.Find(eventId);

        if(itemToUpdate is null || eventToAdd is null)
        {
            throw new InvalidOperationException("Item of Event does not exist");
        }

        itemToUpdate.Events.Add(eventToAdd);

        _context.SaveChanges();
    }

    


}