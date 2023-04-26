using Portfolio_API.Data;
using Portfolio_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Services;

public class EventService
{
    private readonly ItemContext _context;

    public EventService(ItemContext context)
    {
        _context = context;
    }

    public List<Event> GetAll()
    {
        return _context.Events
            .Include(e => e.ItemId)
            .AsNoTracking()
            .ToList();
    }

    public void UpdateDate(int eventId,DateTime date)
    {
        var eventToUpdate = _context.Events.Find(eventId);

        if(eventToUpdate is null)
        {
            throw new InvalidOperationException("Event does not exist");
        }

        eventToUpdate.Date = date;
        _context.SaveChanges();
    }
}