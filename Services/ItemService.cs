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
            .Include(i => i.Swaps)
            .AsNoTracking()
            .ToList();
    }

    public Item? GetById(int id)
    {
        return _context.Items
            .Include(i => i.Categories)
            .Include(i => i.Swaps)
            .AsNoTracking()
            .SingleOrDefault(i => i.ItemId == id);
    }

    public List<Item> GetByName(string name)
    {
        return _context.Items
            .Include(i => i.Categories)
            .Include(i => i.Swaps)
            .AsNoTracking()
            .Where(i => i.Name!.Contains(name))
            .ToList();
    }

    public List<Swap>? GetSwap(int id)
    {
        return _context.Items
            .Include(i => i.Swaps)
            .SingleOrDefault(i => i.ItemId == id)?
            .Swaps?.ToList();
    }

    public Item Create(Item newItem)
    {
        var tempItem = new Item();
        tempItem.Name = newItem.Name;
        tempItem.Price = newItem.Price;
        tempItem.Ticks = newItem.Ticks;
        if (newItem.Ticks == 0) tempItem.Ticks = null;

        var savedItem = _context.Items.Add(tempItem);
        if (newItem.Categories is not null) SetCategories(savedItem.Entity, newItem.Categories);
        _context.SaveChanges();

        return savedItem.Entity;
    }

    public void AddSwap(int itemId, Swap swapToAdd)
    {
        var itemToUpdate = _context.Items.Include(i => i.Swaps).SingleOrDefault(i => i.ItemId == itemId);

        if (itemToUpdate is null)
        {
            throw new InvalidOperationException("Item or swap does not exist");
        }

        itemToUpdate.Swaps?.Add(swapToAdd);

        _context.SaveChanges();
    }

    public void Update(int itemId, Item _item)
    {
        var itemToUpdate = _context.Items
                                    .Include(i => i.Categories)
                                    .SingleOrDefault(i => i.ItemId == itemId);

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
    }
}