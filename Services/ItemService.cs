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

    public List<Item> GetAll()
    {
        return _context.Items
            .Include(i => i.Categories)
            .Include(i => i.Events)
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

    public Item? GetByName(string name)
    {
        return _context.Items
            .Include(i => i.Categories)
            .Include(i => i.Events)
            .AsNoTracking()
            .SingleOrDefault(i => i.Name == name);
    }

    public List<Event> GetEvents(int id)
    {
        return _context.Items
            .Single(i => i.ItemId==id)
            .Events;
    }

    public Item Create(Item newItem)
    {
        _context.Items.Add(newItem);
        _context.SaveChanges();

        return newItem;
    }

    public void AddEvent(int itemId, int eventId)
    {
        var itemToUpdate = _context.Items.Find(itemId);
        var eventToAdd = _context.Events.Find(eventId);

        if (itemToUpdate is null || eventToAdd is null)
        {
            throw new InvalidOperationException("Item or Event does not exist");
        }

        itemToUpdate.Events.Add(eventToAdd);

        _context.SaveChanges();
    }
    public void DeleteEvent(int itemId, int eventId)
    {
        var itemToUpdate = _context.Items.Find(itemId);
        var eventToDelete = _context.Events.Find(eventId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item does not exist");
        }

        if (eventToDelete is not null)
        {
            itemToUpdate.Events.Remove(eventToDelete);
            _context.SaveChanges();
        }
    }

    public void AddCategory(int itemId, int categoryId)
    {
        var itemToUpdate = _context.Items.Find(itemId);
        var categoryToAdd = _context.Categories.Find(categoryId);

        if (itemToUpdate is null || categoryToAdd is null)
        {
            throw new InvalidOperationException("Item or category does not exist");
        }

        itemToUpdate.Categories.Add(categoryToAdd);
        _context.SaveChanges();
    }
    public void DeleteCategory(int itemId, int categoryId)
    {
        var itemToUpdate = _context.Items.Find(itemId);
        var categoryToDelete = _context.Categories.Find(categoryId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item does not exist");
        }

        if (categoryToDelete is not null)
        {
            itemToUpdate.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
        }
    }

    public void UpdatePrice(int itemId, decimal _price)
    {
        var itemToUpdate = _context.Items.Find(itemId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item does not exist");
        }

        itemToUpdate.Price = _price;

        _context.SaveChanges();
    }
    public void UpdateSwapFrequency(int itemId, int _frequency)
    {
        var itemToUpdate = _context.Items.Find(itemId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item does not exist");
        }

        itemToUpdate.SwapFrequency = _frequency;

        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var itemToDelete = _context.Items.Find(id);

        if (itemToDelete is not null)
        {
            _context.Items.Remove(itemToDelete);
            _context.SaveChanges();
        }
    }




}