using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class InvitationRepository : IInvitationRepository
{
    protected AppContext Context { get; set; }
    
    public InvitationRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Create(Invitation invitation)
    {
        Context.Invitations.Add(invitation);
        Context.SaveChanges();
    }
    
    public List<Invitation> GetAll()
    {
        return Context.Invitations.ToList();
    }
    
    public Invitation GetById(int id)
    {
        return Context.Invitations.Find(id);
    }
    
    public void Delete(Invitation invitation)
    {
        Context.Set<Invitation>().Remove(invitation);
        Context.SaveChanges();
    }
    
    public void Update(Invitation invitation)
    {
        Context.Entry(invitation).State = EntityState.Modified;
        Context.SaveChanges();
    }
}