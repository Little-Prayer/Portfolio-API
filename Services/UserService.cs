using Portfolio_API.Data;
using Portfolio_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Services;

public class UserService
{
    private readonly ItemContext _context;

    public UserService(ItemContext context)
    {
        _context = context;
    }

    public List<User> GetAll()
    {
        return _context.Users
            .Include(u => u.Categories)
            .Include(u => u.Items)
            .Include(u => u.Swaps)
            .AsNoTracking()
            .ToList();
    }

    public User? GetById(int id)
    {
        return _context.Users
            .Include(u => u.Categories)
            .Include(u => u.Items)
            .Include(u => u.Swaps)
            .AsNoTracking()
            .SingleOrDefault(u => u.UserId == id);
    }

    public User? GetByEmail(string address)
    {
        return _context.Users
            .Include(u => u.Categories)
            .Include(u => u.Items)
            .Include(u => u.Swaps)
            .AsNoTracking()
            .SingleOrDefault(u => u.UserEmail == address);
    }


}