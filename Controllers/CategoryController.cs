using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Services;
using Portfolio_API.Models;

namespace Portfolio_API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Category>> GetAll()=> _service.GetAll();
    
    [HttpGet("{id}")]
    public ActionResult<Category> GetById(int id)
    {
        var category = _service.GetById(id);
        if(category == null) return NotFound();
        return category;
    }

    [HttpPost]
    public IActionResult Create(Category newCategory)
    {
        var _category = _service.Create(newCategory);
        return CreatedAtAction(nameof(GetById),new{id = _category.CategoryId},_category);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateName(int id,string _name)
    {
        try
        {
            _service.UpdateName(id,_name);
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