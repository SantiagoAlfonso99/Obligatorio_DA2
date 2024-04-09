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
}