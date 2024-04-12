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
        return NotFound();
    }

    [HttpPost]
    public IActionResult Create(MaintenanceCreateModel newStaff)
    {
        MaintenanceStaff returnedStaff =  staffLogic.Create(newStaff.ToEntity());
        return Ok(new MaintenanceStaffDetailModel(returnedStaff));
    }
}