namespace Domain.Models;

public class Invitation
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime DeadLine { get; set; }
    public int CreatorId { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Invitation other = (Invitation)obj;
        return Id == other.Id
            && Email == other.Email
            && Name == other.Name
            && DeadLine == other.DeadLine
            && CreatorId == other.CreatorId;
    }
}