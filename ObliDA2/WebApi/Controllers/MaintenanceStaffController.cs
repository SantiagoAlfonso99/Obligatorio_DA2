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

    public MaintenanceStaffController(IMaintenanceLogic staffLogicIn)
    {
        staffLogic = staffLogicIn;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(staffLogic.GetAll().Select(staff => new MaintenanceStaffDetailModel(staff)).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        try
        {
            return Ok(new MaintenanceStaffDetailModel(staffLogic.GetById(id)));
        }
        catch(InvalidStaffLogicException)
        {
            return NotFound(new { Message = "No MaintenanceStaff was found with that ID." });
        }
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
        try
        {
            if (newStaff.AssociatedBuilding == null)
            {
                return BadRequest(new { Message = "Ensure not to input empty or null data." });     
            }
            MaintenanceStaff returnedStaff = staffLogic.Create(newStaff.ToEntity());
            return Ok(new MaintenanceStaffDetailModel(returnedStaff));
        }
        catch (InvalidStaffLogicException)
        {
            return BadRequest(new { Message = "There is already maintenance staff with that email, please enter another one." });
        }
        catch (InvalidUserException)
        {
            return BadRequest(new { Message = "Ensure not to input empty or null data." });
        }
    }
}