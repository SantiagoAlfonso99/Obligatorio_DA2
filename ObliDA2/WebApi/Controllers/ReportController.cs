using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Exceptions;
using WebApi.DTOs.In;
using WebApi.Filters;

namespace WebApi.Controllers;

[ApiController]
[Route("api/reports")]
[ManagerAuthorization]
public class ReportController: ControllerBase
{
    private readonly IReportLogic reportLogic;

    public ReportController(IReportLogic reportLogicIn)
    {
        reportLogic = reportLogicIn;
    }
    
    [HttpGet("building")]
    public IActionResult GetRequestPerBuilding([FromBody] string buildingName)
    {
        return Ok(reportLogic.CreateRequestsPerBuildingReports(buildingName));
    }
    
    [HttpGet("maintenance")]
    public IActionResult GetRequestsPerMaintenanceStaff([FromBody] MaintenanceRequestModel requestModel)
    {
        return Ok(reportLogic.CreateRequestsPerMaintenanceStaffReports(requestModel.WorkerName, requestModel.BuildingId));
    }

}