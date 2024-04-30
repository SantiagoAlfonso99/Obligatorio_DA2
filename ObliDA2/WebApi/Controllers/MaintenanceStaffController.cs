using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using WebApi.Filters;
namespace WebApi.Controllers;

[ApiController]
[Route("api/MaintenanceStaff")]
[BaseAuthorization("Manager")]

public class MaintenanceStaffController : ControllerBase
{
    private IMaintenanceLogic staffLogic;
    private IBuildingLogic buildingLogic;
    private IUsersLogic userLogic;
    public MaintenanceStaffController(IMaintenanceLogic staffLogicIn, IBuildingLogic buildingLogicIn, IUsersLogic userLogicIn)
    {
        staffLogic = staffLogicIn;
        buildingLogic = buildingLogicIn;
        userLogic = userLogicIn;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(staffLogic.GetAll().Select(staff => new MaintenanceStaffDetailModel(staff)).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(new MaintenanceStaffDetailModel(staffLogic.GetById(id)));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = staffLogic.Delete(id);
        if (success)
        {
            return NoContent();    
        }
        return NotFound(new { Message = "There is no maintenance staff for that Id"});
    }

    [HttpPost]
    public IActionResult Create(MaintenanceCreateModel newStaff)
    {
        var newStaffModel = newStaff.ToEntity();
        userLogic.ValidateEmail(newStaffModel.Email);
        newStaffModel.AssociatedBuilding = buildingLogic.GetById(newStaff.AssociatedBuildingId);
        return Ok(new MaintenanceStaffDetailModel(staffLogic.Create(newStaffModel)));
    }
}