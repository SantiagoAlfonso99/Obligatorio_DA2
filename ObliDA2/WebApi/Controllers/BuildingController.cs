using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;

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
}