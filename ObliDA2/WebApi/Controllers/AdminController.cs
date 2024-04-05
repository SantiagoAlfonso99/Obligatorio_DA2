using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
using Domain.Exceptions;
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
        try
        {
            return Ok(new AdminDetailModel(adminLogic.GetById(id)));
        }
        catch (InvalidAdminException ex)
        {
            return BadRequest(new { Message = "The update action could not be completed because there is no admin with that ID" });
        }
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] AdminCreateModel newAdmin)
    {
        try
        {
            var createdAdmin = adminLogic.Create(newAdmin.ToEntity());
            return Ok(new AdminDetailModel(createdAdmin));
        }
        catch (InvalidUserException ex)
        {
            return BadRequest(new { Message = "Please ensure that all fields are completed and that you provide an email address in the format x@x.com." });
        }
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AdminUpdateModel newAttributes)
    {
        try
        {
            var returnedAdmin = adminLogic.Update(id, newAttributes.ToEntity());
            return Ok(new AdminDetailModel(returnedAdmin));
        }
        catch (InvalidUserException ex)
        {
            return BadRequest(new { Message = "All fields are required." });
        }
        catch (InvalidAdminException ex)
        {
            return NotFound(new { Message = "The update action could not be completed because there is no admin with that ID" });
        }
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