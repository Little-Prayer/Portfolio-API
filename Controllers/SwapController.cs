using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Services;
using Portfolio_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace Portfolio_API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
public class SwapController : ControllerBase
{
    private readonly SwapService _service;
    public SwapController(SwapService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Swap>> GetAll() =>
        _service.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Swap> GetById(int id)
    {
        var _swap = _service.GetById(id);
        return _swap is null ? NotFound() : _swap;
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDate(int id, [FromQuery] DateTime date)
    {
        try
        {
            _service.UpdateDate(id, date);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    [HttpPut("{id}/Memo")]
    public IActionResult UpdateMemo(int id, [FromBody] String Memo)
    {
        try
        {
            _service.UpdateMemo(id, Memo);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<Swap> DeleteById(int id)
    {
        try
        {
            var deletedSwap = _service.DeleteById(id);
            return deletedSwap;
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}