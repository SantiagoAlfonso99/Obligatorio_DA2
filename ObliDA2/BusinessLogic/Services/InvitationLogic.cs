using BusinessLogic.IRepository;
using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
    
namespace BusinessLogic.Services;

public class InvitationLogic : IInvitationLogic
{
    private const string AcceptedStatus = "Accepted";
    private const string RejectedStatus = "Rejected";
    private const string PendingStatus = "Pending";
    
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
            invitation.RecipientEmail == invitationData.RecipientEmail && invitation.DeadLine > DateTime.Now
            && invitation.Status != RejectedStatus);
        if (repeatedInvitation)
        {
            throw new DuplicateEntryException();
        }
        Invitation newInvitation = new Invitation()
        {
            DeadLine = invitationData.DeadLine, RecipientEmail = invitationData.RecipientEmail, CreatorId = invitationData.CreatorId,
            Name = invitationData.Name, Status = PendingStatus
        };
        invitationRepo.Create(newInvitation);
        return newInvitation;
    }

    public bool Delete(int id)
    {
        Invitation invitationToRemove = invitationRepo.GetById(id);
        if (invitationToRemove == null || invitationToRemove.Status == AcceptedStatus)
        {
            return false;
        }
        invitationRepo.Delete(invitationToRemove);
        return true;
    }

    public Invitation InvitationResponse(int id, string email, bool answer)
    {
        Invitation invitationToUpdate = invitationRepo.GetById(id);
        if (invitationToUpdate == null)
        {
            throw new NotFoundException();
        }
        if (answer && invitationToUpdate.RecipientEmail == email)
        {
            invitationToUpdate.Status = AcceptedStatus;
        }
        else if(invitationToUpdate.RecipientEmail == email && !answer)
        {
            invitationToUpdate.Status = RejectedStatus;
        }
        invitationRepo.Update(invitationToUpdate);
        return invitationToUpdate;
    }
}