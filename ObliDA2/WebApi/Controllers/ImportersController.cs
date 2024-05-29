using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/importers")]
public class ImportersController : ControllerBase
{
    private readonly IImporterLogic _importerLogic;

    public ImportersController(IImporterLogic importerLogic)
    {
        _importerLogic = importerLogic;
    }
        
    [HttpGet]
    public IActionResult Index()
    {
        var availableImporters = _importerLogic.GetAllImporters();
        return Ok(availableImporters.Select(i => i.GetName()).ToList());
    }
}