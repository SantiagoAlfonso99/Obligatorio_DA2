using Domain.Models;
namespace WebApi.DTOs.In;

public class ManagerCreateModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }   
    
    public Manager ToEntity()
    {
        return new Manager()
        {
            Name = this.Name,
            LastName = this.LastName,
            Password = this.Password,
            Email = this.Email
        };
    }
}