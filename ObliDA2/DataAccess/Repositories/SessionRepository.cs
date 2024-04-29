using Domain.Models;

namespace DataAccess.Repositories;

public class UserRepository
{
    protected AppContext Context { get; set; }
    
    public UserRepository(AppContext context)
    {
        Context = context;
    }

    public User? FindByToken(Guid token)
    {
        var session = Context.Set<Session>().FirstOrDefault(s => s.Token == token);
        return session.User;
    }
}