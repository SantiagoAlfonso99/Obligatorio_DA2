namespace WebApi.DTOs.In;

public class InvitationResponse
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool acceptInvitation { get; set; }
}