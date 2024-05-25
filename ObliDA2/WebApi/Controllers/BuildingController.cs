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
[Route("api/buildings")]

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
        List<Building> buildings = new List<Building>() {};
        var companyAdmin = (CompanyAdmin) usersLogic.GetCurrentUser();
        if (companyAdmin.Company != null)
        {
            var company = companyAdmin.Company;
            buildings = buildingLogic.GetAll().Where(building => building.Company.Equals(company)).ToList();   
        }
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
        int managerId = usersLogic.GetCurrentUser().Id;
        bool success = buildingLogic.Delete(id, managerId);
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
        int managerId = usersLogic.GetCurrentUser().Id;
        return Ok(new BuildingDetailModel(buildingLogic.Update(id, newAttributes.ToEntity(), managerId)));
    }
    
    [BaseAuthorization("CompanyAdmin")]
    [HttpPut("{id}/manager")]
    public IActionResult UpdateBuildingManager(int id, [FromBody] BuildingUpdateManager newManager)
    {
        var admin = (CompanyAdmin) usersLogic.GetCurrentUser();
        var returnedManager = managerLogic.GetById(newManager.ManagerId);
        var building = buildingLogic.GetById(id);
        if (admin.Company == null || admin.Company.Id != building.CompanyId)
        {
            return BadRequest(new { Message = "You don't have the necessary permission to modify the building." });
        }
        building.BuildingManager = returnedManager;
        return Ok(new BuildingDetailModel(buildingLogic.UpdateManager(building)));
    }
}