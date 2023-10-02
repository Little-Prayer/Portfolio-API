using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Services;
using Portfolio_API.Models;

namespace Portfolio_API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<User>> GetAll() => _service.GetAll();

    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        var user = _service.GetById(id);

        if (user == null) return NotFound();

        return user;
    }

    [HttpGet("data")]
    public ActionResult<User> GetByName([FromQuery] string email)
    {
        var user = _service.GetByEmail(email);

        if (user == null) return NotFound();

        return user;
    }

    [HttpPost]
    public IActionResult Create([FromBody]User newUser)
    {
        var _user = _service.Create(newUser);
        return CreatedAtAction(nameof(GetById), new { id = _user.UserId }, _user);
    }
}