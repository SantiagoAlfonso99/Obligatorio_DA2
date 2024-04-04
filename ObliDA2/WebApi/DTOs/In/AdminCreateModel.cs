using Domain.Models;
namespace WebApi.DTOs.In;

public class AdminCreateModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Admin ToEntity()
    {
        return new Admin()
        {
            Name = this.Name,
            LastName = this.LastName,
            Password = this.Password,
            Email = this.Email
        };
    }
}