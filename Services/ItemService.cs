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

    public List<Item> GetByName(string name)
    {
        return _context.Items
            .Include(i => i.Categories)
            .Include(i => i.Events)
            .AsNoTracking()
            .Where(i => i.Name!.Contains(name))
            .ToList();
    }

    public List<Event>? GetEvents(int id)
    {
        return _context.Items
            .Include(i => i.Events)
            .SingleOrDefault(i => i.ItemId == id)?
            .Events?.ToList();
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

        itemToUpdate.Events!.Add(eventToAdd);

        _context.SaveChanges();
    }

    public void Update(int itemId, Item _item)
    {
        var itemToUpdate = _context.Items.Find(itemId);

        Console.WriteLine(itemId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item does not exist");
        }

        itemToUpdate.Name = _item.Name ?? itemToUpdate.Name;
        itemToUpdate.Price = _item.Price ?? itemToUpdate.Price;
        itemToUpdate.Ticks = _item.Ticks ?? itemToUpdate.Ticks;
        if (_item.Ticks == 0) itemToUpdate.Ticks = null;
        if (_item.Categories is not null) SetCategories(itemToUpdate, _item.Categories);

        _context.SaveChanges();
    }

    public Item DeleteById(int id)
    {
        var itemToDelete = _context.Items.Find(id);

        if (itemToDelete is not null)
        {
            _context.Items.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }
        else
        {
            throw new InvalidOperationException("Item does not exist");
        }
    }

    public void SetCategories(Item itemToCategoriesSet, ICollection<Category> categoriesToSet)
    {
        if (itemToCategoriesSet.Categories is null)
        {
            itemToCategoriesSet.Categories = new HashSet<Category>();
        }
        else
        {
            foreach (var catToDel in itemToCategoriesSet.Categories)
            {
                itemToCategoriesSet.Categories.Remove(catToDel);
            }
        }

        foreach (var catToAdd in categoriesToSet)
        {
            var catToSet = _context.Categories.Find(catToAdd.CategoryId);
            itemToCategoriesSet.Categories.Add(catToSet!);
        }
        _context.SaveChanges();
    }




}