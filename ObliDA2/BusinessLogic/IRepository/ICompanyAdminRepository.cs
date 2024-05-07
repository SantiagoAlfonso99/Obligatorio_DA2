using Domain.Models;

namespace BusinessLogic.IRepository;

public interface ICompanyAdminRepository
{
    List<CompanyAdmin> GetAll();
}