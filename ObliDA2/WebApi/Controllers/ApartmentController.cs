using IBusinessLogic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using WebApi.Filters;
using Domain.Models;
namespace WebApi.Controllers;

[ApiController]
[Route("api/apartments")]

public class ApartmentController : ControllerBase
{
    private IApartmentLogic apartmentLogic;
    private IBuildingLogic buildingLogic;
    private IApartmentOwnerLogic ownerLogic;
    private IUsersLogic usersLogic;

    public ApartmentController(IApartmentLogic apartmentLogicIn, IBuildingLogic buildingLogicIn, IApartmentOwnerLogic ownerLogicIn
    ,IUsersLogic usersLogicIn)
    {
        apartmentLogic = apartmentLogicIn;
        buildingLogic = buildingLogicIn;
        ownerLogic = ownerLogicIn;
        usersLogic = usersLogicIn;
    }

    [BaseAuthorization("Manager")]
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(apartmentLogic.GetAll().Select(apartment => new ApartmentDetailModel(apartment)).ToList());
    }

    [BaseAuthorization("Manager")]
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(new ApartmentDetailModel(apartmentLogic.GetById(id)));
    }
    
    [BaseAuthorization("CompanyAdmin")]
    [HttpPost]
    public IActionResult Create([FromBody] ApartmentCreateModel newApartment)
    {
        var newApartmentModel = newApartment.ToEntity();
        var user = (CompanyAdmin) usersLogic.GetCurrentUser();
        newApartmentModel.Building = buildingLogic.GetById(newApartment.BuildingId);
        if (user.CompanyId != newApartmentModel.Building.CompanyId)
        {
            return BadRequest(new { Message = "You do not have access to the specified building." });
        }
        newApartmentModel.Owner = ownerLogic.GetById(newApartment.OwnerId);
        newApartmentModel.BuildingId = newApartment.BuildingId;
        return Ok(new ApartmentDetailModel(apartmentLogic.Create(newApartmentModel)));
    }
    
    [BaseAuthorization("Manager")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = (Manager) usersLogic.GetCurrentUser();
        var apartment = apartmentLogic.GetById(id);
        var building = buildingLogic.GetById(apartment.BuildingId);
        if (building.BuildingManagerId != user.Id)
        {
            return BadRequest(new { Message = "You do not have access to the specified building." });
        }
        apartmentLogic.Delete(apartment);
        return NoContent();
    }
}