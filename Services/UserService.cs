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

    public User? Create(User newUser)
    {
        _context.Users.Add(newUser);
        _context.SaveChanges();

        return newUser;
    }

    public void UpdateEmail(int userId,string email)
    {
        var userToUpdate = _context.Users.Find(userId) ?? throw new InvalidOperationException("User does not exist");

        userToUpdate.UserEmail = email;
        _context.SaveChanges();
    }

    public User DeleteUser(int id)
    {
        var userToDelete = _context.Users.Find(id) ?? throw new InvalidCastException("User does not exist");

        _context.Users.Remove(userToDelete);
        _context.SaveChanges();

        return userToDelete;
    }
}