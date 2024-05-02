using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class ApartmentRepository : IApartmentRepository
{
    protected AppContext Context { get; set; }
    
    public ApartmentRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Create(Apartment apartment)
    {
        Context.Apartments.Add(apartment);
        Context.SaveChanges();
    }
    
    public List<Apartment> GetAll()
    {
        return Context.Apartments.ToList();
    }
    
    public Apartment GetById(int id)
    {
        return Context.Apartments.Find(id);
    }
    
    public void Delete(Apartment apartment)
    {
        Context.Set<Apartment>().Remove(apartment);
        Context.SaveChanges();
    }
}