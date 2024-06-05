using Domain.Models;
namespace IBusinessLogic;

public interface IUsersLogic
{
    public void ValidateEmail(string email);
    public User? GetCurrentUser(Guid? token = null);
    public User LogIn(string email, string password);
    public Guid CreateSession(Session session);
}