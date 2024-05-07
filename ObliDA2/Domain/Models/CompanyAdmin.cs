namespace Domain.Models;

public class CompanyAdmin : User
{

    public override string GetRole()
    {
        return "CompanyAdmin";
    }
}