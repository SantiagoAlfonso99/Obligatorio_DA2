using Domain.Models;

namespace BusinessLogic.IRepository;

public interface ISessionRepository
{
    public string? FindByToken(Guid token);
    public void Create(Session session);
}