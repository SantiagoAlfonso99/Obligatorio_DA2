using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using Domain.Models;
using Microsoft.Extensions.FileProviders;
using WebApi.Filters;
namespace WebApi.Controllers;

[ApiController]
[Route("api/building")]

public class BuildingController : ControllerBase
{
    private IBuildingLogic buildingLogic;
    private IManagerLogic managerLogic;
    private IUsersLogic usersLogic;

    public BuildingController(IBuildingLogic logicIn, IManagerLogic managerLogicIn, IUsersLogic usersLogicIn)
    {
        buildingLogic = logicIn;
        usersLogic = usersLogicIn;
        managerLogic = managerLogicIn;
    }
    
    [BaseAuthorization("CompanyAdmin")]
    [HttpGet]
    public IActionResult Index()
    {
        var companyAdmin = (CompanyAdmin) usersLogic.GetCurrentUser();
        var company = companyAdmin.Company;
        var buildings = buildingLogic.GetAll().FindAll(building => building.Company.Equals(company));
        return Ok(buildings.Select(building => new BuildingDetailModel(building)).ToList());
    }
    
    [BaseAuthorization("CompanyAdmin")]
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(new BuildingDetailModel(buildingLogic.GetById(id)));
    }
    
    [BaseAuthorization("CompanyAdmin")]
    [HttpPost]
    public IActionResult Create([FromBody] BuildingCreateModel newBuilding)
    {
        var building = newBuilding.ToEntity();
        var companyAdmin = (CompanyAdmin)usersLogic.GetCurrentUser();
        building.Company = companyAdmin.Company;
        if (building.Company == null)
        {
            return BadRequest(new { Message = "The building could not be created because the admin does not have an associated construction company." });
        }
        return Ok(new BuildingDetailModel(buildingLogic.Create(building)));
    }
    
    [BaseAuthorization("Manager")]
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
    
    [BaseAuthorization("Manager")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] BuildingUpdateModel newAttributes)
    {
        return Ok(new BuildingDetailModel(buildingLogic.Update(id, newAttributes.ToEntity())));
    }
    
    [BaseAuthorization("CompanyAdmin")]
    [HttpPut("{id}/manager")]
    public IActionResult UpdateBuildingManager(int id, [FromBody] BuildingUpdateManager newManager)
    {
        var returnedManager = managerLogic.GetById(newManager.ManagerId);
        var building = buildingLogic.GetById(id);
        building.BuildingManager = returnedManager;
        return Ok(new BuildingDetailModel(buildingLogic.UpdateManager(building)));
    }
}