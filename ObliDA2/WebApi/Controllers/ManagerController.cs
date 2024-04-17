using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using System.Linq;

namespace WebApi.Controllers;

[ApiController]
[Route("api/manager")]
public class ManagerController : ControllerBase
{
    private readonly IManagerLogic _managerLogic;

    public ManagerController(IManagerLogic managerLogic)
    {
        _managerLogic = managerLogic;
    }

    [HttpPost]
    public IActionResult Create([FromBody] ManagerCreateModel newRequest)
    {
        try
        {
            var createdManager = _managerLogic.Create(newRequest.ToEntity());
            return Ok(new ManagerDetailModel(createdManager));
        }
        catch (InvalidUserException ex)
        {
            return BadRequest(new { Message = "Validation failed. Please ensure all fields are correctly filled." });
        }
    }
    
    [HttpGet("requests")]
    public IActionResult GetRequests(string category)
    {
        var requests = _managerLogic.ViewRequests(category);
        var response = requests.Select(r => new RequestDetailModel(r)).ToList();
        return Ok(response);
    }

    [HttpPost("requests")]
    public IActionResult CreateRequest([FromBody] RequestCreateModel requestDto)
    {
        try
        {
            var request = _managerLogic.CreateRequest(requestDto.Description, requestDto.Department, requestDto.Category);
            var response = new RequestDetailModel(request);
            return CreatedAtAction(nameof(GetRequests), new { id = request.Id }, response);
        }
        catch (InvalidRequestException ex)
        {
            return BadRequest(new { Message = "Failed to create request due to invalid data." });
        }
    }

    [HttpPost("assign")]
    public IActionResult AssignRequest([FromBody] RequestAssignModel assignRequestDto)
    {
        try
        {
            _managerLogic.AssignRequestToMaintenance(assignRequestDto.RequestId, assignRequestDto.MaintenanceId);
            return Ok(new { Message = "Request successfully assigned." });
        }
        catch (InvalidRequestException)
        {
            return BadRequest(new { Message = "Unable to assign request due to invalid IDs or request/maintenance status." });
        }
    }
}