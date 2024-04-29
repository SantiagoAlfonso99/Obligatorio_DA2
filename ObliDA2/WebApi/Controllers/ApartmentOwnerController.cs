using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using WebApi.Filters;
namespace WebApi.Controllers;

[ApiController]
[Route("api/ApartmentOwners")]
[ManagerAuthorization]

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
        return Ok(new ApartmentOwnerDetailModel(ownerLogic.GetById(id)));
    }
    
    [HttpPost]
    public IActionResult Create(ApartmentOwnerCreateModel owner)
    {
        return Ok(new ApartmentOwnerDetailModel(ownerLogic.Create(owner.ToEntity())));
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, ApartmentOwnerCreateModel newAttributes)
    {
        return Ok(new ApartmentOwnerDetailModel(ownerLogic.Update(id, newAttributes.ToEntity())));
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = ownerLogic.Delete(id);
        if (success)
        {
            return NoContent();
        }
        return NotFound(new { Message = "The deletion action could not be completed because there is no apartment owner with that ID" });
    }
}