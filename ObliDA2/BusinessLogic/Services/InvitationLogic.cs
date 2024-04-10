using BusinessLogic.IRepository;
using IBusinessLogic;
using Domain.Models;
    
namespace BusinessLogic.Services;

public class InvitationLogic
{
    private IInvitationRepository invitationRepo;

    public InvitationLogic(IInvitationRepository invitationRepoIn)
    {
        invitationRepo = invitationRepoIn;
    }

    public List<Invitation> GetAll()
    {
        return invitationRepo.GetAll();
    }
    
}