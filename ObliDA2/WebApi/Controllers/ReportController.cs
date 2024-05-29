using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Exceptions;
using WebApi.DTOs.In;
using WebApi.Filters;

namespace WebApi.Controllers;

[ApiController]
[Route("api/reports")]
[BaseAuthorization("Manager")]
public class ReportController: ControllerBase
{
    private readonly IReportLogic reportLogic;

    public ReportController(IReportLogic reportLogicIn)
    {
        reportLogic = reportLogicIn;
    }
    
    [HttpGet("building")]
    public IActionResult GetRequestPerBuilding([FromBody] BuildingReportDTO buildingReport)
    {
        return Ok(reportLogic.CreateRequestsPerBuildingReports(buildingReport.BuildingName));
    }
    
    [HttpGet("maintenance")]
    public IActionResult GetRequestsPerMaintenanceStaff([FromBody] MaintenanceRequestModel requestModel)
    {
        return Ok(reportLogic.CreateRequestsPerMaintenanceStaffReports(requestModel.WorkerName, requestModel.BuildingId));
    }

}