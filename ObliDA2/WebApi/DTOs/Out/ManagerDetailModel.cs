using Domain.Models;

namespace WebApi.DTOs.Out;
public class ManagerDetailModel
{
    public ManagerDetailModel(Manager managerIn)
    {
        Id = managerIn.Id;
        Name = managerIn.Name;
        Password = managerIn.Password;
        Email = managerIn.Email;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ManagerDetailModel(Request request)
    {
        Id = request.Id;
        Description = request.Description;
        Department = request.Department;
        Category = request.Category;
        Status = request.Status.ToString();
    }
}