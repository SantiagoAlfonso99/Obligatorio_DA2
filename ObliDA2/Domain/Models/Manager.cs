namespace Domain.Models;

public class Manager
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } 
    
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
               && Password == other.Password
               && Email == other.Email;
    }
}