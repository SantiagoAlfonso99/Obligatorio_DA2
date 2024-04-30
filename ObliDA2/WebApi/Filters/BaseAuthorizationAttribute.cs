using IBusinessLogic;
using Domain.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class BaseAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public string RequiredRole { get; set; }

    public BaseAuthorizationAttribute(string requiredRole)
    {
        RequiredRole = requiredRole;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"];
        
        if (String.IsNullOrEmpty(token))
        {
            context.Result = new JsonResult("Empty authorization header") { StatusCode = 401 };
        } 
        else if (!Guid.TryParse(token, out Guid parsedToken))
        {
            context.Result = new JsonResult("Invalid token format") { StatusCode = 400 };
        }
        else
        {
            var currentUser = GetSessionLogicService(context).GetCurrentUser(parsedToken);
            
            if (currentUser == null)
            {
                context.Result = new JsonResult("Inicie sesión") { StatusCode = 401 };
            }
            bool roleMatch = RequiredRole.Equals(currentUser.GetRole());
            if (!roleMatch)
            {
                context.Result = new JsonResult($"Access Denied for {RequiredRole} only") { StatusCode = 403 };
            }
        }
    }

    IUsersLogic? GetSessionLogicService(AuthorizationFilterContext context)
    {
        var sessionManagerObject = context.HttpContext.RequestServices.GetService(typeof(IUsersLogic));
        return sessionManagerObject as IUsersLogic;
    }
}