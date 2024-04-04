using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
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
        List<Admin> returnedList =  adminLogic.GetAll();
        return Ok(returnedList.Select(admin => new AdminDetailModel(admin)).ToList());
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        Admin admin = adminLogic.GetById(id);
        return Ok(new AdminDetailModel(admin));
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] AdminCreateModel newAdmin)
    {
        Admin createdAdmin = adminLogic.Create(newAdmin.ToEntity());
        return Ok(new AdminDetailModel(createdAdmin));
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AdminUpdateModel newAttributes)
    {
        Admin returnedAdmin = adminLogic.Update(id, newAttributes.ToEntity());
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
        return NotFound(new { Message = "Error" });
    }
}