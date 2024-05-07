using BusinessLogic.IRepository;
using Domain.Exceptions;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class CompanyAdminLogic : ICompanyAdminLogic
{
    private readonly ICompanyAdminRepository companyAdminRepository;
    private readonly IConstructionCompanyRepository companyRepo;
    
    public CompanyAdminLogic(ICompanyAdminRepository repoIn, IConstructionCompanyRepository companyRepoIn)
    {
        companyAdminRepository = repoIn;
        companyRepo = companyRepoIn;
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
    
    public ConstructionCompany CreateCompany(ConstructionCompany newModel)
    {
        companyRepo.Create(newModel);
        return newModel;
    }
    
    public ConstructionCompany GetById(int id)
    {
        ConstructionCompany company = companyRepo.GetById(id);
        if (company == null)
        {
            throw new NotFoundException();
        }
        return company;
    }
}