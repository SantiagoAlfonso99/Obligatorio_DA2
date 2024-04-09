using Domain.Models;
namespace IBusinessLogic;

public interface IInvitationLogic
{
    List<Invitation> GetAll();
    Invitation GetById(int id);
}