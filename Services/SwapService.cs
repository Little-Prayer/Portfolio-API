using Portfolio_API.Data;
using Portfolio_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_API.Services;

public class SwapService
{
    private readonly ItemContext _context;

    public SwapService(ItemContext context)
    {
        _context = context;
    }

    public List<Swap> GetAll()
    {
        return _context.Swaps
            .Include(e => e.Item)
            .AsNoTracking()
            .ToList();
    }

    public Swap? GetById(int SwapId)
    {
        return _context.Swaps
                        .Include(e=>e.Item)
                        .AsNoTracking()
                        .SingleOrDefault(e=>e.SwapId == SwapId);
    }

    public Swap Create(Swap newSwap)
    {
        _context.Swaps.Add(newSwap);
        _context.SaveChanges();

        return newSwap;
    }

    public void UpdateDate(int swapId, DateTime _date)
    {
        var swapToUpdate = _context.Swaps.Find(swapId);

        if (swapToUpdate is null)
        {
            throw new InvalidOperationException("Swap does not exist");
        }

        swapToUpdate.Date = _date;
        _context.SaveChanges();
    }

    public void UpdateMemo(int swapId, String _Memo)
    {
        var swapToUpdate = _context.Swaps.Find(swapId);

        if (swapToUpdate is null)
        {
            throw new InvalidOperationException("Swap does not exist");
        }

        swapToUpdate.Memo = _Memo;
        _context.SaveChanges();
    }
    public Swap DeleteById(int Id)
    {
        var swapToDelete = _context.Swaps.Find(Id);

        if (swapToDelete is not null)
        {
            _context.Swaps.Remove(swapToDelete);
            _context.SaveChanges();
            return swapToDelete;
        }
        else
        {
            throw new InvalidOperationException("Swap does not exist");
        }
    }
}