using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Exceptions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportController: ControllerBase
{
    private readonly IReportLogic reportLogic;

    public ReportController(IReportLogic reportLogicIn)
    {
        reportLogic = reportLogicIn;
    }
    
    [HttpGet]
    public IActionResult GetRequestPerBuilding([FromBody] string buildingName)
    {
        return Ok(reportLogic.CreateRequestsPerBuildingReports(buildingName));
    }
    
    [HttpGet]
    public IActionResult GetRequestsPerMaintenanceStaff([FromBody] string workerName, [FromBody] int buildingId)
    {
        return Ok(reportLogic.CreateRequestsPerMaintenanceStaffReports(workerName, buildingId));
    }
}