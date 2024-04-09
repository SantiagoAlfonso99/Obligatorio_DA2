using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
using IBusinessLogic;

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
        return Ok(invitationLogic.GetAll());
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        try
        {
            var invitation = invitationLogic.GetById(id);
            return Ok(invitation);
        }
        catch (InvalidInvitationException ex)
        {
            return NotFound(new { Message = "The show action could not be completed because there is no invitation with that ID" });
        }
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Invitation newInvitation)
    {
        try
        {
            var createdInvitation = invitationLogic.Create(newInvitation);
            return Ok(createdInvitation);
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
    public IActionResult InvitationResponse(int id, [FromBody] string email, [FromBody] string newPassword, [FromBody] bool acceptInvitation)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest(new { Message = "Please ensure to enter a non-null or non-empty email and password" });    
            }
            var invitation = invitationLogic.InvitationResponse(id, email, newPassword, acceptInvitation);
            return Ok(invitation);
        }
        catch (InvalidInvitationException ex)
        {
            return NotFound(new { Message = "Error, invitation not found." });
        }
    }
}