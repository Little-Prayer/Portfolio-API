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
            .Include(e => e.Item)
            .AsNoTracking()
            .ToList();
    }

    public Event Create(Event newEvent)
    {
        _context.Events.Add(newEvent);
        _context.SaveChanges();

        return newEvent;
    }

    public void UpdateDate(int eventId,DateTime _date)
    {
        var eventToUpdate = _context.Events.Find(eventId);

        if(eventToUpdate is null)
        {
            throw new InvalidOperationException("Event does not exist");
        }

        eventToUpdate.Date = _date;
        _context.SaveChanges();
    }
    public void DeleteById(int Id)
    {
        var eventToDelete = _context.Events.Find(Id);

        if(eventToDelete is not null)
        {
            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("Event does not exist");
        }
    }
}