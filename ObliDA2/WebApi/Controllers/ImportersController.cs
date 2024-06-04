using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using WebApi.DTOs.Out;

namespace WebApi.Controllers;

[ApiController]
[Route("api/importers")]
public class ImportersController : ControllerBase
{
    private readonly IImporterLogic _importerLogic;
    private readonly IUsersLogic _usersLogic;

    public ImportersController(IImporterLogic importerLogic, IUsersLogic _usersLogicIn)
    {
        _importerLogic = importerLogic;
        _usersLogic = _usersLogicIn;
    }
        
    [HttpGet]
    public IActionResult Index()
    {
        var availableImporters = _importerLogic.GetAllImporters();
        return Ok(availableImporters.Select(i => i.GetName()).ToList());
    }
    
    [HttpGet("{name}")]
    public IActionResult ImportBuildings(string name)
    {
        List<Building> buildings = new List<Building>();
        var companyAdmin = (CompanyAdmin)_usersLogic.GetCurrentUser();
        if (companyAdmin != null && companyAdmin.Company != null)
        {
            buildings = _importerLogic.ImportBuildings(name,companyAdmin.Company);
        }
        return Ok(buildings.Select(building => new BuildingDetailModel(building)).ToList());
    }
}