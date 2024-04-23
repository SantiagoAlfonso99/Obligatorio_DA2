namespace Domain.Models;

public class Manager : User
{
    public override string Role => "manager";
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Manager other = (Manager)obj;
        return Id == other.Id
               && Name == other.Name
               && Email == other.Email
               && Password == other.Password;
    }
}