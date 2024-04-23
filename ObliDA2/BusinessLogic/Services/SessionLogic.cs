using BusinessLogic.IRepository;
using Domain.Models;

namespace BusinessLogic.Services;

public class SessionLogic
{
    private readonly IUserRepository _repository;
    private User? _currentUser;

    public SessionLogic(IUserRepository repository)
    {
        _repository = repository;
    }

    public User? GetCurrentUser(Guid? token = null)
    {
        if (token == null)
        {
            return _currentUser;
        }

        return _repository.FindByToken(token.Value);
    }
}