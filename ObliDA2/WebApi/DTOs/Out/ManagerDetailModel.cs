using Domain.Models;
namespace WebApi.DTOs.Out;

public class ManagerDetailModel
{
    public ManagerDetailModel(Manager managerIn)
    {
        Id = managerIn.Id;
        Name = managerIn.Name;
        LastName = managerIn.LastName;
        Password = managerIn.Password;
        Email = managerIn.Email;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}