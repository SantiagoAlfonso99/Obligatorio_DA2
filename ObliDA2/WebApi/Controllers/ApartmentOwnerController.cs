using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/ApartmentOwners")]
public class ApartmentOwnerController : ControllerBase
{
    private IApartmentOwnerLogic ownerLogic;

    public ApartmentOwnerController(IApartmentOwnerLogic logicIn)
    {
        ownerLogic = logicIn;
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(ownerLogic.GetById(id));
    }
}