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
            return Ok(invitationLogic.GetById(id));
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Invitation newInvitation)
    {
            var createdInvitation = invitationLogic.Create(newInvitation);
            return Ok(createdInvitation);
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
        var invitation = invitationLogic.InvitationResponse(id, email, newPassword, acceptInvitation);
        return Ok(invitation);
    }
}