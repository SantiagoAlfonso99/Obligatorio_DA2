namespace Domain.Models;

public class CompanyAdmin : User
{

    public override string GetRole()
    {
        return "CompanyAdmin";
    }
    
    public override bool Equals(object obj)
    {
        CompanyAdmin other = (CompanyAdmin)obj;
        return Id == other.Id
               && Name == other.Name
               && Password == other.Password
               && Email == other.Email;
    }
}