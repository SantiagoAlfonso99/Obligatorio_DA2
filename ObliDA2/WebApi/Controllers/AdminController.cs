using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
namespace WebApi.Controllers;

[ApiController]
[Route("api/admins")]
public class AdminController : ControllerBase
{
    private readonly IAdminLogic adminLogic;

    public AdminController(IAdminLogic adminLogicIn)
    {
        adminLogic = adminLogicIn;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(adminLogic.GetAll().Select(admin => new AdminDetailModel(admin)).ToList());
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(new AdminDetailModel(adminLogic.GetById(id)));
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] AdminCreateModel newAdmin)
    {
        var createdAdmin = adminLogic.Create(newAdmin.ToEntity());
        return Ok(new AdminDetailModel(createdAdmin));
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AdminUpdateModel newAttributes)
    {
        var returnedAdmin = adminLogic.Update(id, newAttributes.ToEntity());
        return Ok(new AdminDetailModel(returnedAdmin));
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool success = adminLogic.Delete(id);
        if (success)
        {
            return NoContent();   
        }
        return NotFound(new { Message = "The deletion action could not be completed because there is no admin with that ID" });
    }
}