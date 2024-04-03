using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using Domain.Models;
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
        return Ok(adminLogic.GetAll());
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        Admin admin = adminLogic.GetById(id);
        return Ok(admin);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Admin newAdmin)
    {
        Admin createdAdmin = adminLogic.Create(newAdmin);
        return Ok(newAdmin);
    }
}