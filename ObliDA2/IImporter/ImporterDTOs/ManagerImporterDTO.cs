namespace IImporter;
using Domain.Models;
public class ManagerImporterDTO
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public Manager ToEntity()
    {
        return new Manager()
        {
            Name = this.Name,
            Password = this.Password,
            Email = this.Email,
        };
    }
}