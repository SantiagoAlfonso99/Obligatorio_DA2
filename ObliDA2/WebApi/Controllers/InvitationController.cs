using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace WebApi.Controllers;

[ApiController]
[Route("api/invitations")]
public class InvitationController : ControllerBase
{
    private readonly IInvitationLogic invitationLogic;
    
    public InvitationController(IInvitationLogic invitationLogicIn)
    {
        invitationLogic = invitationLogicIn;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(invitationLogic.GetAll().Select(invitation => new InvitationDetailModel(invitation)).ToList());
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        try
        {
            var invitation = invitationLogic.GetById(id);
            return Ok(new InvitationDetailModel(invitation));
        }
        catch (InvalidInvitationException ex)
        {
            return NotFound(new { Message = "The show action could not be completed because there is no invitation with that ID" });
        }
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] InvitationCreateModel newInvitation)
    {
        try
        {
            var createdInvitation = invitationLogic.Create(newInvitation.ToEntity());
            return Ok(new InvitationDetailModel(createdInvitation));
        }
        catch (InvalidInvitationException ex)
        {
            return BadRequest(new { Message = "Make sure not to insert null values and that there is no other invitation for the same recipient" });
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = invitationLogic.Delete(id);
        if (success)
        {
            return NoContent();   
        }
        return NotFound(new { Message = "The deletion action could not be completed because there is no invitation with that ID" });
    }
    
    [HttpPut("{id}")]
    public IActionResult InvitationResponse(int id, [FromBody] InvitationResponse response)
    {
        try
        {
            if (string.IsNullOrEmpty(response.Email) || string.IsNullOrEmpty(response.Password))
            {
                return BadRequest(new { Message = "Please ensure to enter a non-null or non-empty email and password" });    
            }
            var invitation = invitationLogic.InvitationResponse(id, response.Email, response.acceptInvitation);
            return Ok(new InvitationDetailModel(invitation));
        }
        catch (InvalidInvitationLogicException ex)
        {
            return NotFound(new { Message = "Error, invitation not found." });
        }
    }
}