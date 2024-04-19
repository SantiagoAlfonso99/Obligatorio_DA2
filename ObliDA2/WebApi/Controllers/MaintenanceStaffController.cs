using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace WebApi.Controllers;

[ApiController]
[Route("api/MaintenanceStaff")]
public class MaintenanceStaffController : ControllerBase
{
    private IMaintenanceLogic staffLogic;
    private IBuildingLogic buildingLogic;

    public MaintenanceStaffController(IMaintenanceLogic staffLogicIn, IBuildingLogic buildingLogicIn)
    {
        staffLogic = staffLogicIn;
        buildingLogic = buildingLogicIn;
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
        newStaffModel.AssociatedBuilding = buildingLogic.GetById(newStaff.AssociatedBuildingId);
        MaintenanceStaff returnedStaff = staffLogic.Create(newStaffModel);
        return Ok(new MaintenanceStaffDetailModel(returnedStaff));
    }
}