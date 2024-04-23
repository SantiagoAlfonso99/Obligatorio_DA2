using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using WebApi.Filters;

namespace WebApi.Controllers;

[ApiController]
[Route("api/invitations")]
[AdminAuthorization]
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
        return Ok(new InvitationDetailModel(invitation));
    }
}