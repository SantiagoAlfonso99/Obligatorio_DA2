using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IUserRepository
{
    public User? FindByToken(Guid token);
}