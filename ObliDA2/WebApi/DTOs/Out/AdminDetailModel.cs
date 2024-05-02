using Domain.Models;
namespace WebApi.DTOs.Out;

public class AdminDetailModel
{
    public AdminDetailModel(Admin adminIn)
    {
        Id = adminIn.Id;
        Name = adminIn.Name;
        LastName = adminIn.LastName;
        Password = adminIn.Password;
        Email = adminIn.Email;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public override bool Equals(object? obj)
    {
        var otherAdminDetail = obj as AdminDetailModel;
        return Id == otherAdminDetail.Id
               && Name == otherAdminDetail.Name
               && LastName == otherAdminDetail.LastName
               && Password == otherAdminDetail.Password
               && Email == otherAdminDetail.Email;
    }
}