namespace Domain.Models;

public class Building
{
    public int Id { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Building other = (Building)obj;
        return Id == other.Id;
    }
}