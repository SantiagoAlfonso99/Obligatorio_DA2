namespace Domain.Models;

public class Invitation
{
    public int Id { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Invitation other = (Invitation)obj;
        return Id == other.Id;
    }
}