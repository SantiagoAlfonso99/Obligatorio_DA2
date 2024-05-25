using Domain.Models;
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

public class InvitationController : ControllerBase
{
    private readonly IInvitationLogic invitationLogic;
    private readonly IManagerLogic managerLogic;
    private readonly ICompanyAdminLogic companyAdminLogic;
    private readonly IUsersLogic usersLogic;
    
    public InvitationController(IInvitationLogic invitationLogicIn, IManagerLogic managerLogicIn, ICompanyAdminLogic companyAdminLogicIn, IUsersLogic userLogicIn)
    {
        invitationLogic = invitationLogicIn;
        managerLogic = managerLogicIn;
        companyAdminLogic = companyAdminLogicIn;
        usersLogic = userLogicIn;
    }
    
    [BaseAuthorization("Admin")]
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(invitationLogic.GetAll().Select(invitation => new InvitationDetailModel(invitation)).ToList());
    }
    
    [BaseAuthorization("Admin")]
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        var invitation = invitationLogic.GetById(id); 
        return Ok(new InvitationDetailModel(invitation));
    }
    
    [BaseAuthorization("Admin")]
    [HttpPost]
    public IActionResult Create([FromBody] InvitationCreateModel newInvitation)
    {
        if (newInvitation.Role != "CompanyAdmin" && newInvitation.Role != "Manager")
        {
            return BadRequest(new { Message = "Invalid role name" }); 
        }
        var createdInvitation = invitationLogic.Create(newInvitation.ToEntity());
        return Ok(new InvitationDetailModel(createdInvitation));
    }
    
    [BaseAuthorization("Admin")]
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
        usersLogic.ValidateEmail(response.Email);
        var invitation = invitationLogic.InvitationResponse(id, response.Email, response.acceptInvitation);
        if (invitation.DeadLine < DateTime.Now)
        {
            return BadRequest(new { Message = "Invitation expired" });    
        }
        if (invitation.Role == "Manager" && invitation.Status != "Rejected")
        {
            managerLogic.Create(new Manager() { Email = response.Email, Password = response.Password, Name = invitation.Name});
        }
        else if(invitation.Status != "Rejected")
        {
            companyAdminLogic.Create(new CompanyAdmin() { Email = response.Email, Password = response.Password, Name = invitation.Name});
        }
        return Ok(new InvitationDetailModel(invitation));
    }
}