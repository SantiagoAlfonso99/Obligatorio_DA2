using Domain.Models;
namespace IBusinessLogic;

public interface IInvitationLogic
{
    List<Invitation> GetAll();
    Invitation GetById(int id);
    Invitation Create(Invitation invitation);
    bool Delete(int id);
}