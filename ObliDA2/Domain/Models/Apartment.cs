namespace Domain.Models;

public class Apartment
{
    public int Id { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Apartment other = (Apartment)obj;
        return Id == other.Id;
    }
}