using IBusinessLogic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using WebApi.Filters;
namespace WebApi.Controllers;

[ApiController]
[Route("api/apartments")]
[BaseAuthorization("Manager")]

public class ApartmentController : ControllerBase
{
    private IApartmentLogic apartmentLogic;
    private IBuildingLogic buildingLogic;
    private IApartmentOwnerLogic ownerLogic;

    public ApartmentController(IApartmentLogic apartmentLogicIn, IBuildingLogic buildingLogicIn, IApartmentOwnerLogic ownerLogicIn)
    {
        apartmentLogic = apartmentLogicIn;
        buildingLogic = buildingLogicIn;
        ownerLogic = ownerLogicIn;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(apartmentLogic.GetAll().Select(apartment => new ApartmentDetailModel(apartment)).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(new ApartmentDetailModel(apartmentLogic.GetById(id)));
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ApartmentCreateModel newApartment)
    {
        var newApartmentModel = newApartment.ToEntity();
        newApartmentModel.Building = buildingLogic.GetById(newApartment.BuildingId);
        newApartmentModel.Owner = ownerLogic.GetById(newApartment.OwnerId);
        newApartmentModel.BuildingId = newApartment.BuildingId;
        return Ok(new ApartmentDetailModel(apartmentLogic.Create(newApartmentModel)));
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = apartmentLogic.Delete(id);
        if (success)
        {
            return NoContent();
        }
        return NotFound(new { Message = "There is no apartment for that Id"});
    }
}