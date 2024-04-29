using BusinessLogic.IRepository;
using Domain.Models;

namespace DataAccess.Repositories;

public class SessionRepository : ISessionRepository
{
    protected AppContext Context { get; set; }
    
    public SessionRepository(AppContext context)
    {
        Context = context;
    }
    public User? FindByToken(Guid token)
    {
        var session = Context.Set<Session>().FirstOrDefault(s => s.Token == token);
        return session?.User;
    }
}