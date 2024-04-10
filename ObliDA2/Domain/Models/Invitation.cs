using System.Runtime.InteropServices.JavaScript;
using Domain.Exceptions;
namespace Domain.Models;

public class Invitation
{
    private const string AcceptedStatus = "Accepted";
    private const string RejectedStatus = "Rejected";
    private const string PendingStatus = "Pending";
    
    private int id;
    private string recipientEmail;
    private string name;
    private string status;
    private int creatorId;
    private DateTime deadLine;
    
    public int Id { get; set; }
    public string RecipientEmail
    {
        get => recipientEmail;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidInvitationException();
            }
            recipientEmail = value;
        }
    }
    
    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidInvitationException();
            }
            name = value;
        }
    }
    
    public DateTime DeadLine
    {
        get => deadLine;
        set
        {
            if (value < DateTime.Now)
            {
                throw new InvalidInvitationException();
            }
            deadLine = value;
        }
    }
    
    public int CreatorId { get; set; }
    
    public string Status
    {
        get => status;
        set
        {
            if (string.IsNullOrEmpty(value) || InvalidStatus(value))
            {
                throw new InvalidInvitationException();
            }
            status = value;
        }
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Invitation other = (Invitation)obj;
        return Id == other.Id
               && RecipientEmail == other.RecipientEmail
               && Name == other.Name
               && DeadLine == other.DeadLine
               && CreatorId == other.CreatorId
               && Status == other.Status;
    }

    private bool InvalidStatus(string statusIn)
    {
        bool invalid = (statusIn != AcceptedStatus && statusIn != RejectedStatus && statusIn != PendingStatus);
        return invalid;
    }
}