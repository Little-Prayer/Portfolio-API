using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Services;
using Portfolio_API.Models;

namespace Portfolio_API.Controllers;

[ApiController]
[Route("controller")]
public class ItemController : ControllerBase
{

    private readonly ItemService _service;
    public ItemController(ItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Item>> GetAll() =>
        _service.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Item> GetById(int id)
    {
        var item = _service.GetById(id);

        if (item == null)
            return NotFound();

        return item;
    }

    [HttpGet("data")]
    public ActionResult<Item> GetByName([FromQuery]string name)
    {
        var item = _service.GetByName(name);

        if (item == null)
            return NotFound();

        return item;
    }

    [HttpGet("{id}/event")]
    public ActionResult<List<Event>> GetEvents(int id)
    {
        return _service.GetEvents(id);
    }

    [HttpGet("{id}/event/latest")]
    public ActionResult<Event> GetLatestEvent(int id)
    {
        var ev= _service.GetEvents(id)
            .OrderByDescending(e=>e.Date)
            .FirstOrDefault();

        if(ev == null)return NotFound();

        return ev;
    }

    [HttpPost]
    public IActionResult Create(Item newItem)
    {
        var _item = _service.Create(newItem);
        return CreatedAtAction(nameof(GetById),new{id = _item!.ItemId},_item);
    }

}