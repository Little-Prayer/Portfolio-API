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
            .AsNoTracking()
            .ToList();
    }

    public Category? GetById(int categoryId)
    {
        return _context.Categories
            .AsNoTracking()
            .SingleOrDefault(c => c.CategoryId==categoryId);
    } 

    public Category? GetByName(string name)
    {
        return _context.Categories
            .AsNoTracking()
            .SingleOrDefault(c => c.Name==name);
    }
}