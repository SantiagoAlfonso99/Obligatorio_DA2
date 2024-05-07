using Domain.Models;

namespace IBusinessLogic;

public interface ICompanyAdminLogic
{
    CompanyAdmin Create(CompanyAdmin companyAdmin);
    List<CompanyAdmin> GetAll();
    ConstructionCompany CreateCompany(ConstructionCompany newModel);
    ConstructionCompany GetById(int id);
}