using BusinessLogic.Exceptions;
namespace BusinessLogic.Models;

public class Admin
{
    private string name;

    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidAdminException();
            }
        }
    }

    public int Id { get; set; }
}