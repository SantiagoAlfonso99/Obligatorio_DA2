using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
using IBusinessLogic;

namespace WebApi.Controllers;


[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private ICategoryLogic categoryLogic;

    public CategoryController(ICategoryLogic categoryLogicIn)
    {
        categoryLogic = categoryLogicIn;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(categoryLogic.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(categoryLogic.GetById(id));
    }

    [HttpPost]
    public IActionResult Create([FromBody] string name)
    {
        return Ok(categoryLogic.Create(name));
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = categoryLogic.Delete(id);
        return NoContent();;
    }
}