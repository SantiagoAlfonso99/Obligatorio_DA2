using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using WebApi.Filters;

namespace WebApi.Controllers;

[ApiController]
[Route("api/companyController")]
[BaseAuthorization("CompanyAdmin")]

public class ConstructionCompanyController : ControllerBase
{
    private ICompanyAdminLogic adminLogic;
    private IUsersLogic usersLogic;

    public ConstructionCompanyController(ICompanyAdminLogic adminLogicIn, IUsersLogic usersLogicIn)
    {
        adminLogic = adminLogicIn;
        usersLogic = usersLogicIn;
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] CompanyCreateModel newCompany)
    {
        CompanyAdmin? companyAdmin = (CompanyAdmin?) usersLogic.GetCurrentUser();
        if (companyAdmin != null && companyAdmin.Company != null)
        {
            return BadRequest(new { Message = "An administrator of companies can only create a single construction company." });
        }
        return Ok(new CompanyConstructionDetailModel(adminLogic.CreateCompany(newCompany.ToEntity(), companyAdmin)));
    }
}