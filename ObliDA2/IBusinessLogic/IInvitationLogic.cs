using Domain.Models;
namespace IBusinessLogic;

public interface IInvitationLogic
{
    List<Invitation> GetAll();
}