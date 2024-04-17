using IBusinessLogic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace WebApi.Controllers;

[ApiController]
[Route("api/apartments")]
public class ApartmentController : ControllerBase
{
    private IApartmentLogic apartmentLogic;

    public ApartmentController(IApartmentLogic apartmentLogicIn)
    {
        apartmentLogic = apartmentLogicIn;
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
    public IActionResult Create(ApartmentCreateModel newApartment)
    {
        return Ok(new ApartmentDetailModel(apartmentLogic.Create(newApartment.ToEntity())));
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