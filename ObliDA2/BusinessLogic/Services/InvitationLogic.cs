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
        invitationData.Status = PendingStatus;
        invitationRepo.Create(invitationData);
        return invitationData;
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
        if (invitationToUpdate == null || invitationToUpdate.RecipientEmail != email || invitationToUpdate.Status != "Pending")
        {
            throw new NotFoundException();
        }
        invitationToUpdate.Status = answer ? AcceptedStatus : RejectedStatus;
        invitationRepo.Update(invitationToUpdate);
        return invitationToUpdate;
    }
}