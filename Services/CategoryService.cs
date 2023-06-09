using Portfolio_API.Data;
using Portfolio_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Services;

public class CategoryService
{
    private readonly ItemContext _context;

    public CategoryService(ItemContext context)
    {
        _context = context;
    }

    public List<Category> GetAll()
    {
        return _context.Categories
            .Include(c => c.Items)
            .AsNoTracking()
            .ToList();
    }

    public Category? GetById(int categoryId)
    {
        return _context.Categories
            .Include(c => c.Items)
            .ThenInclude(i=>i.Events)
            .AsNoTracking()
            .SingleOrDefault(c => c.CategoryId == categoryId);
    }

    public Category? GetByName(string name)
    {
        return _context.Categories
            .Include(c => c.Items)
            .AsNoTracking()
            .SingleOrDefault(c => c.Name == name);
    }

    public Category Create(Category newCategory)
    {
        _context.Categories.Add(newCategory);
        _context.SaveChanges();

        return newCategory;
    }

    public void UpdateName(int categoryId, string _name)
    {
        var categoryToRename = _context.Categories.Find(categoryId);

        if (categoryToRename is null)
        {
            throw new InvalidOperationException("Category does not exist");
        }

        categoryToRename.Name = _name;
        _context.SaveChanges();
    }
    public Category DeleteById(int Id)
    {
        var categoryToDelete = _context.Categories.Find(Id);

        if (categoryToDelete is not null)
        {
            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
            return categoryToDelete;
        }
        else
        {
            throw new InvalidCastException("category does not exist");
        }
    }
}