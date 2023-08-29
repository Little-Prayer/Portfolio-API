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

    
}