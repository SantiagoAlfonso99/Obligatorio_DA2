using Domain.Models;
namespace WebApi.DTOs.Out;

public class InvitationDetailModel
{
    public InvitationDetailModel(Invitation invitationIn)
    {
        Id = invitationIn.Id;
        Name = invitationIn.Name;
        Email = invitationIn.RecipientEmail;
        DeadLine = invitationIn.DeadLine;
        CreatorId = invitationIn.CreatorId;
        Status = invitationIn.Status;
    }
    
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime DeadLine { get; set; }
    public int CreatorId { get; set; }
    
    public string Status { get; set; }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        InvitationDetailModel other = (InvitationDetailModel)obj;
        return Id == other.Id
               && Email == other.Email
               && Name == other.Name
               && DeadLine == other.DeadLine
               && CreatorId == other.CreatorId
               && Status == other.Status;
    }
}