using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class BaseAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public string RequiredRole { get; }

    public BaseAuthorizationAttribute(string requiredRole)
    {
        RequiredRole = requiredRole;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"];
        if (String.IsNullOrEmpty(token))
        {
            context.Result = new JsonResult("Empty Authorization") { StatusCode = 401 };
            return;
        }

        if (!Guid.TryParse(token, out Guid parsedToken))
        {
            context.Result = new JsonResult("Invalid Token Format") { StatusCode = 401 };
            return;
        }

        var sessionLogic = GetSessionLogicService(context);
        var currentUser = sessionLogic?.GetCurrentUser(parsedToken);
        if (currentUser == null)
        {
            context.Result = new JsonResult("Please LogIn") { StatusCode = 401 };
            return;
        }

        {
            context.Result = new JsonResult($"Access Denied for {RequiredRole} only") { StatusCode = 403 };
            return;
        }
    }

    private SessionLogic? GetSessionLogicService(AuthorizationFilterContext context)
    {
        var sessionManagerObject = context.HttpContext.RequestServices.GetService(typeof(SessionLogic));
        return sessionManagerObject as SessionLogic;
    }
}
