using System.Runtime.InteropServices.JavaScript;
using Domain.Exceptions;
namespace Domain.Models;

public class Invitation
{
    private const string AcceptedStatus = "Accepted";
    private const string RejectedStatus = "Rejected";
    private const string PendingStatus = "Pending";
    
    public int id;
    public string recipientEmail;
    public string name;
    public string status;
    public int creatorId;
    public DateTime deadLine;
    
    public int Id { get; set; }
    public string RecipientEmail
    {
        get => recipientEmail;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyOrNullException();
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
                throw new EmptyOrNullException();
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
                throw new ArgumentException();
            }
            deadLine = value;
        }
    }
    
    public string Status
    {
        get => status;
        set
        {
            if (string.IsNullOrEmpty(value) || InvalidStatus(value))
            {
                throw new ArgumentException();
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
               && Status == other.Status;
    }

    private bool InvalidStatus(string statusIn)
    {
        bool invalid = (statusIn != AcceptedStatus && statusIn != RejectedStatus && statusIn != PendingStatus);
        return invalid;
    }
}