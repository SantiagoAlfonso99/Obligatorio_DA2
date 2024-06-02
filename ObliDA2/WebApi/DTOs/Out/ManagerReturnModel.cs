using Domain.Models;

namespace WebApi.DTOs.Out;
public class ManagerReturnModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    public ManagerReturnModel(Manager manager)
    {
        Id = manager.Id;
        Email = manager.Email;
        Name = manager.Name;
    }
    
    public override bool Equals(object obj)
    {
        ManagerReturnModel other = (ManagerReturnModel)obj;
        return Id == other.Id
               && Email == other.Email
               && Name == other.Name;
    }
}