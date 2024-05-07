using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class CompanyAdminRepository : ICompanyAdminRepository
{
    protected AppContext Context { get; set; }
    
    public CompanyAdminRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Create(CompanyAdmin companyAdmin)
    {
        Context.CompanyAdmins.Add(companyAdmin);
        Context.SaveChanges();
    }
    
    public List<CompanyAdmin> GetAll()
    {
        return Context.CompanyAdmins.ToList();
    }
}