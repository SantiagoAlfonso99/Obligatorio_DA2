using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Exceptions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportController: ControllerBase
{
    private readonly IManagerLogic managerLogic;
    
}