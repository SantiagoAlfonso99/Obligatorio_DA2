using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.Identity.Client;
using WebApi.DTOs.In;
using WebApi.Filters;

namespace WebApi.Controllers;

[ApiController]
[Route("api/session")]
public class SessionController : ControllerBase
{
    private readonly IUsersLogic userLogic;

    public SessionController(IUsersLogic userLogicIn)
    {
        userLogic = userLogicIn;
    }
    
    [HttpPost]
    public IActionResult LogIn([FromBody] LogInDTO loginDto)
    {
        var returnedUser = userLogic.LogIn(loginDto.Email, loginDto.Password);
        var newSession = new Session();
        newSession.UserEmail = returnedUser.Email;
        var token = userLogic.CreateSession(newSession);
        return Ok(new { token = token });
    }
}