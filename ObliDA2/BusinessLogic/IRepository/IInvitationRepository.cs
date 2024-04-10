using Domain.Models;
namespace BusinessLogic.IRepository;

public interface IInvitationRepository
{
    List<Invitation> GetAll();
}