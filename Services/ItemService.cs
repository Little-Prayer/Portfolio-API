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

    public List<Event>? GetEvents(int id)
    {
        return _context.Items
            .Include(i=> i.Events)
            .SingleOrDefault(i => i.ItemId==id)?
            .Events;
    }

    public Item Create(Item newItem)
    {
        _context.Items.Add(newItem);
        _context.SaveChanges();

        return newItem;
    }

    public void AddEvent(int itemId, Event eventToAdd)
    {
        var itemToUpdate = _context.Items.Find(itemId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item or Event does not exist");
        }

        itemToUpdate.Events.Add(eventToAdd);

        _context.SaveChanges();
    }

        public void UpdateName(int itemId, string _name)
    {
        var itemToUpdate = _context.Items.Find(itemId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item does not exist");
        }

        itemToUpdate.Name = _name;

        _context.SaveChanges();
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
    public void UpdateSwapFrequency(int itemId, long _frequency)
    {
        var itemToUpdate = _context.Items.Find(itemId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item does not exist");
        }

        itemToUpdate.Ticks = _frequency;

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
        else
        {
            throw new InvalidOperationException("Item does not exist");
        }
    }




}