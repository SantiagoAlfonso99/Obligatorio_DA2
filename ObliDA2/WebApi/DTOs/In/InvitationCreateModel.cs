using Domain.Models;
namespace WebApi.DTOs.In;

public class InvitationCreateModel
{
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime DeadLine { get; set; }
    public int CreatorId { get; set; }
    
    public Invitation ToEntity()
    {
        return new Invitation()
        {
            Email = this.Email,
            Name = this.Name,
            DeadLine = this.DeadLine,
            CreatorId = this.CreatorId
        };
    }
}