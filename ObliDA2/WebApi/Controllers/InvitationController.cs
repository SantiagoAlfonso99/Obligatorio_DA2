using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace WebApi.Controllers;

[ApiController]
[Route("api/invitations")]
public class InvitationController : ControllerBase
{
    private readonly IInvitationLogic invitationLogic;
    private readonly IManagerLogic managerLogic;
    
    public InvitationController(IInvitationLogic invitationLogicIn, IManagerLogic managerLogicIn)
    {
        invitationLogic = invitationLogicIn;
        managerLogic = managerLogicIn;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(invitationLogic.GetAll().Select(invitation => new InvitationDetailModel(invitation)).ToList());
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        var invitation = invitationLogic.GetById(id); 
        return Ok(new InvitationDetailModel(invitation));
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] InvitationCreateModel newInvitation)
    {
        var createdInvitation = invitationLogic.Create(newInvitation.ToEntity());
        return Ok(new InvitationDetailModel(createdInvitation));
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
        if (string.IsNullOrEmpty(response.Email) || string.IsNullOrEmpty(response.Password))
        {
            return BadRequest(new { Message = "Please ensure to enter a non-null or non-empty email and password" });    
        }
        var invitation = invitationLogic.InvitationResponse(id, response.Email, response.acceptInvitation);
        if (invitation.DeadLine < DateTime.Now)
        {
            return BadRequest(new { Message = "Invitation expired" });    
        }
        managerLogic.Create(new Manager() { Email = response.Email, Password = response.Password, Name = invitation.Name});
        return Ok(new InvitationDetailModel(invitation));
    }
}