using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;


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
        return Ok(categoryLogic.GetAll().Select(category => new CategoryDetailModel(category)).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(new CategoryDetailModel(categoryLogic.GetById(id)));
    }

    [HttpPost]
    public IActionResult Create([FromBody] CategoryCreateModel newCategory)
    {
        CategoryDetailModel returnedCategory = new CategoryDetailModel(categoryLogic.Create(newCategory.ToEntity()));
        return Ok(returnedCategory);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = categoryLogic.Delete(id);
        if (success)
        {
            return NoContent();
        }
        return NotFound(new { Message = "The deletion action could not be completed because there is no Category with that ID" });
    }
}