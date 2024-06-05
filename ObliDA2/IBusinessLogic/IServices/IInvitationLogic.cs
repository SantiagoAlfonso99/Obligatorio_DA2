using Domain.Models;
namespace IBusinessLogic;

public interface IInvitationLogic
{
    List<Invitation> GetAll();
    Invitation GetById(int id);
    Invitation Create(Invitation invitation);
    Invitation InvitationResponse(int id, string email, bool acceptInvitation);
    bool Delete(int id);
}