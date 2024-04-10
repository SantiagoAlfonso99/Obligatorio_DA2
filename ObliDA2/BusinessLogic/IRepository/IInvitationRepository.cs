using Domain.Models;
namespace BusinessLogic.IRepository;

public interface IInvitationRepository
{
    List<Invitation> GetAll();
    Invitation GetById(int id);
    void Create(Invitation newInvitation);
    void Delete(Invitation invitationToRemove);
    void Update(Invitation newAttributes);
}