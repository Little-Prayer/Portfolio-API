using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Services;
using Portfolio_API.Models;

namespace Portfolio_API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly EventService _service;
    public EventController(EventService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Event>> GetAll()=>
        _service.GetAll();

    [HttpPut("{id}")]
    public IActionResult UpdateDate(int id,[FromQuery]DateTime date)
    {
        try
        {
            _service.UpdateDate(id,date);
            return NoContent();
        }
        catch(InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        try
        {
            _service.DeleteById(id);
            return NoContent();
        }
        catch(InvalidOperationException)
        {
            return NotFound();
        }
    }
}