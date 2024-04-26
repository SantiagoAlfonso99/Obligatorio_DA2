using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Exceptions;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using System.Linq;
using Domain.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/manager")]
public class ManagerController : ControllerBase
{
    private readonly IManagerLogic _managerLogic;
    private readonly ICategoryLogic _categoryLogic;
    private readonly IApartmentLogic _apartmentLogic;
    private readonly IMaintenanceLogic _maintenanceLogic;

    public ManagerController(IManagerLogic managerLogic, ICategoryLogic catLogicIn, IApartmentLogic apartLogicIn
    , IMaintenanceLogic _maintenanceLogicIn)
    {
        _managerLogic = managerLogic;
        _categoryLogic = catLogicIn;
        _apartmentLogic = apartLogicIn;
        _maintenanceLogic = _maintenanceLogicIn;
    }

    [HttpGet("requests")]
    public IActionResult GetRequests(string category)
    {
        var requests = _managerLogic.ViewRequests(category);
        var response = requests.Select(r => new ManagerDetailModel(r)).ToList();
        return Ok(response);
    }
    
    [HttpPost("requests")]
    public IActionResult CreateRequest([FromBody] ManagerCreateModel requestDto)
    {
        var category = _categoryLogic.GetById(requestDto.CategoryId);
        var apartment = _apartmentLogic.GetById(requestDto.DepartmentId);
        var request = _managerLogic.CreateRequest(requestDto.Description, apartment, category);
        return Ok(new ManagerDetailModel(request));
    }

    [HttpPost("assign")]
    public IActionResult AssignRequest([FromBody] ManagerAssignModel assignRequestDto)
    {
        var worker =_maintenanceLogic.GetById(assignRequestDto.MaintenanceId);
        bool success = _managerLogic.AssignRequestToMaintenance(assignRequestDto.RequestId, worker);
        if (success)
        {
            return Ok(new { Message = "Request successfully assigned." });   
        }
        return BadRequest(new { Message = "Assignment could not be completed"});
    }
}