namespace Domain.Models;

public class Manager : User
{
    public override bool Equals(object obj)
    {
        Manager other = (Manager)obj;
        return Id == other.Id
               && Name == other.Name
               && Password == other.Password
               && Email == other.Email;
    }

    public override string GetRole()
    {
        return "Manager";
    }
}