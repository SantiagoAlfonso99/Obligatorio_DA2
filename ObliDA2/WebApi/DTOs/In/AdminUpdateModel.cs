using Domain.Models;
namespace WebApi.DTOs.In;

public class AdminUpdateModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public Admin ToEntity()
    {
        return new Admin()
        {
            Name = this.Name,
            LastName = this.LastName,
            Password = this.Password
        };
    }
}