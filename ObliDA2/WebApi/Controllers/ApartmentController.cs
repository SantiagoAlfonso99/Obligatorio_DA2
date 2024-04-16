using IBusinessLogic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/apartment")]
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
        return Ok(apartmentLogic.GetAll());
    }
}