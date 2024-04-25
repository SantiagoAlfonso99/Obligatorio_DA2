using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class ApartmentOwnerRepository : IApartmentOwnerRepository
{
    protected AppContext Context { get; set; }
    
    public ApartmentOwnerRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Create(ApartmentOwner owner)
    {
        Context.Owners.Add(owner);
    }
    
    public List<ApartmentOwner> GetAll()
    {
        return Context.Owners.ToList();
    }
    
    public ApartmentOwner GetById(int id)
    {
        return Context.Owners.Find(id);
    }
    
    public void Delete(ApartmentOwner owner)
    {
        Context.Set<ApartmentOwner>().Remove(owner);
    }
    
    public void Update(ApartmentOwner owner)
    {
        Context.Entry(owner).State = EntityState.Modified;
    }
    
}