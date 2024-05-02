using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Exceptions;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using System.Linq;
using Domain.Models;
using WebApi.Filters;
namespace WebApi.Controllers;

[ApiController]
[Route("api/manager")]

public class ManagerController : ControllerBase
{
    private readonly IManagerLogic _managerLogic;
    private readonly ICategoryLogic _categoryLogic;
    private readonly IApartmentLogic _apartmentLogic;
    private readonly IMaintenanceLogic _maintenanceLogic;
    private readonly IUsersLogic _usersLogic;
    private readonly IBuildingLogic buildingLogic;
    
    public ManagerController(IManagerLogic managerLogic, ICategoryLogic catLogicIn, IApartmentLogic apartLogicIn
    , IMaintenanceLogic _maintenanceLogicIn, IUsersLogic userLogicIn, IBuildingLogic buildingLogicIn)
    {
        _managerLogic = managerLogic;
        _categoryLogic = catLogicIn;
        _apartmentLogic = apartLogicIn;
        _maintenanceLogic = _maintenanceLogicIn;
        _usersLogic = userLogicIn;
        buildingLogic = buildingLogicIn;
    }

    [BaseAuthorization("Manager")]
    [HttpGet("requests")]
    public IActionResult GetRequests(string category)
    {
        var requests = _managerLogic.ViewRequests(category);
        var response = requests.Select(r => new ManagerDetailModel(r)).ToList();
        return Ok(response);
    }
    
    [BaseAuthorization("Manager")]
    [HttpPost("requests")]
    public IActionResult CreateRequest([FromBody] ManagerCreateModel requestDto)
    {
        var category = _categoryLogic.GetById(requestDto.CategoryId);
        var apartment = _apartmentLogic.GetById(requestDto.DepartmentId);
        var building = buildingLogic.GetById(apartment.BuildingId);
        var request = _managerLogic.CreateRequest(requestDto.Description, apartment, category, building);
        return Ok(new ManagerDetailModel(request));
    }

    [BaseAuthorization("Manager")]
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

    [BaseAuthorization("Maintenance")]
    [HttpPut("requests/accept")]
    public IActionResult AcceptRequest([FromBody] AcceptRequestDTO accept)
    {
        Guid? token = null;
        User user = _usersLogic.GetCurrentUser(token);
        Request returnedRequest = _managerLogic.GetAllRequest().ToList().FirstOrDefault(request => request.Id == accept.RequestId);
        if (returnedRequest == null || user == null)
        {
            return NotFound(new {Message = "The user referred to by the token or the given request was not found."});
        }
        if (returnedRequest.AssignedToMaintenanceId == user.Id && returnedRequest.Status == (RequestStatus.Open))
        {
            return Ok(new ManagerDetailModel(_managerLogic.MaintenanceStaffAcceptRequest(returnedRequest, DateTime.Now)));
        }
        return BadRequest(new { Message = "Please verify that this is a request from you and that it is still an open request."});
    }
    
    [BaseAuthorization("Maintenance")]
    [HttpPut("requests/complete")]
    public IActionResult CompleteRequest([FromBody] CompleteRequestDTO completeDTO)
    {
        Guid? token = null;
        User user = _usersLogic.GetCurrentUser(token);
        Request returnedRequest = _managerLogic.GetAllRequest().ToList().FirstOrDefault(request => request.Id == completeDTO.RequestId);
        if (returnedRequest == null || user == null)
        {
            return NotFound(new {Message = "The user referred to by the token or the given request was not found."});
        }
        if (returnedRequest.AssignedToMaintenanceId == user.Id && returnedRequest.Status == (RequestStatus.Attending))
        {
            return Ok(new ManagerDetailModel(_managerLogic.MaintenanceStaffCompleteRequest(returnedRequest,completeDTO.FinalPrice, DateTime.Now)));
        }
        return BadRequest(new { Message = "Please verify that this is a request from you and that it is still an open request."});
    }
}