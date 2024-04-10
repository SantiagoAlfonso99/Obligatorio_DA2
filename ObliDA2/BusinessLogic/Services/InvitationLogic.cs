using BusinessLogic.IRepository;
using Domain.Exceptions;
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

    public Invitation GetById(int id)
    {
        return invitationRepo.GetById(id);
    }

    public Invitation Create(Invitation invitationData)
    {
        List<Invitation> invitations = invitationRepo.GetAll();
        bool repeatedInvitation = invitations.Exists(invitation =>
            invitation.Email == invitationData.Email && invitation.DeadLine > DateTime.Now
            && invitation.Status != "Rejected");
        if (repeatedInvitation)
        {
            throw new InvalidInvitationLogicException();
        }
        Invitation newInvitation = new Invitation()
        {
            DeadLine = invitationData.DeadLine, Email = invitationData.Email, CreatorId = invitationData.CreatorId,
            Name = invitationData.Name, Status = "Pending"
        };
        invitationRepo.Create(newInvitation);
        return newInvitation;
    }
    
}