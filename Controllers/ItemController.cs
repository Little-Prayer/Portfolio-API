using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Services;
using Portfolio_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace Portfolio_API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
public class ItemController : ControllerBase
{

    private readonly ItemService _service;
    public ItemController(ItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Item>> GetAll()
    {
        return new List<Item> ();
    }

        //_service.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Item> GetById(int id)
    {
        var item = _service.GetById(id);

        if (item == null) return NotFound();

        return item;
    }

    [HttpGet("data")]
    public ActionResult<List<Item>> GetByName([FromQuery] string name)
    {
        var items = _service.GetByName(name);

        return items;
    }

    [HttpGet("{id}/swap")]
    public ActionResult<List<Swap>> GetSwap(int id)
    {
        var ev = _service.GetSwap(id);
        if (ev == null) return NotFound();
        return ev;
    }

    [HttpGet("{id}/swap/latest")]
    public ActionResult<Swap> GetLatestSwap(int id)
    {
        var ev = _service.GetSwap(id)?
            .OrderByDescending(e => e.Date)
            .FirstOrDefault();

        if (ev == null) return NotFound();

        return ev;
    }

    [HttpPost]
    public IActionResult Create(Item newItem)
    {
        newItem.ItemId = null;
        var _item = _service.Create(newItem);
        return CreatedAtAction(nameof(GetById), new { id = _item.ItemId }, _item);
    }

    [HttpPost("{id}/swap")]
    public IActionResult AddSwap(int id, [FromBody] Swap ev)
    {
        try
        {
            Console.WriteLine(id);
            _service.AddSwap(id, ev);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Item _item)
    {
        try
        {
            _service.Update(id, _item);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<Item> DeleteById(int id)
    {
        try
        {
            var deletedItem = _service.DeleteById(id);
            return deletedItem;
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}