namespace Domain.Models;

public class Session
{
    public int Id { get; set; }
    public Guid Token { get; set; }
    public string UserEmail { get; set; }

    public Session()
    {
        Token = Guid.NewGuid();
    }
}