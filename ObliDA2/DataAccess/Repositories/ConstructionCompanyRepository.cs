using BusinessLogic.IRepository;
using Domain.Models;

namespace DataAccess.Repositories;

public class ConstructionCompanyRepository : IConstructionCompanyRepository
{
    protected AppContext Context { get; set; }
    
    public ConstructionCompanyRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Create(ConstructionCompany company)
    {
        Context.ConstructionCompanies.Add(company);
        Context.SaveChanges();
    }
    
    public ConstructionCompany GetById(int id)
    {
        return Context.ConstructionCompanies.Find(id);
    }
    
    public List<ConstructionCompany> GetAll()
    {
        return Context.ConstructionCompanies.ToList();
    }
}