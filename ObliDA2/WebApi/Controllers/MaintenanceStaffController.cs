using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;

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
        return Ok(staffLogic.GetAll());
    }
}