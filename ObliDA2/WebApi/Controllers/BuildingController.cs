using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
using Microsoft.Extensions.FileProviders;

namespace WebApi.Controllers;

[ApiController]
[Route("api/building")]
public class BuildingController : ControllerBase
{
    private IBuildingLogic buildingLogic;

    public BuildingController(IBuildingLogic logicIn)
    {
        buildingLogic = logicIn;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(buildingLogic.GetAll());
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(buildingLogic.GetById(id));
    }
    
    [HttpPost]
    public IActionResult Create(Building newBuilding)
    {
        return Ok(buildingLogic.Create(newBuilding));
    }
    
    [HttpDelete]
    public IActionResult Delete([FromBody] Building buildingToRemove)
    {
        bool success = buildingLogic.Delete(buildingToRemove);
        if (success)
        {
            return NoContent();
        }
        return NotFound();
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Building newAttributes)
    {
        return Ok(buildingLogic.Update(id, newAttributes));
    }
}