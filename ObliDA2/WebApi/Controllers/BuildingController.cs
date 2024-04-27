using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using Microsoft.Extensions.FileProviders;

namespace WebApi.Controllers;

[ApiController]
[Route("api/building")]
public class BuildingController : ControllerBase
{
    private IBuildingLogic buildingLogic;
    private IManagerLogic managerLogic;

    public BuildingController(IBuildingLogic logicIn, IManagerLogic managerLogicIn)
    {
        buildingLogic = logicIn;
        managerLogic = managerLogicIn;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(buildingLogic.GetAll().Select(building => new BuildingDetailModel(building)).ToList());
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(new BuildingDetailModel(buildingLogic.GetById(id)));
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] BuildingCreateModel newBuilding)
    {
        var returnedManager = managerLogic.GetById(newBuilding.ManagerAssociatedId);
        var building = newBuilding.ToEntity();
        building.BuildingManager = returnedManager;
        return Ok(new BuildingDetailModel(buildingLogic.Create(building)));
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = buildingLogic.Delete(id);
        if (success)
        {
            return NoContent();
        }
        return NotFound(new { Message = "The deletion action could not be completed because there is no Building with that ID" });
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] BuildingUpdateModel newAttributes)
    {
        return Ok(new BuildingDetailModel(buildingLogic.Update(id, newAttributes.ToEntity())));
    }
}