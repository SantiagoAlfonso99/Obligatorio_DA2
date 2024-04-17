namespace Domain.Models;

public class ApartmentOwner
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        ApartmentOwner other = (ApartmentOwner)obj;
        return Id == other.Id
               && Name == other.Name
               && LastName == other.LastName
               && Email == other.Email;
    }
}