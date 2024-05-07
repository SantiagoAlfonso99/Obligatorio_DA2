using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class CompanyAdminLogic : ICompanyAdminLogic
{
    private readonly ICompanyAdminRepository companyAdminRepository;

    public CompanyAdminLogic(ICompanyAdminRepository repoIn)
    {
        companyAdminRepository = repoIn;
    }

    public CompanyAdmin Create(CompanyAdmin newModel)
    {
        companyAdminRepository.Create(newModel);
        return newModel;
    }

    public List<CompanyAdmin> GetAll()
    {
        return companyAdminRepository.GetAll();
    }
}