using BusinessLogic.Models;
namespace BusinessLogic.Services;

public class AdminLogic
{
    private List<Admin> Admins;

    public AdminLogic()
    {
        Admins = new List<Admin>();
    }
    public void CreateAdmin(int id,string name)
    {
        Admin newAdmin = new Admin(){Id = id, Name = name};
        Admins.Add(newAdmin);
    }

    public int AdminsCount()
    {
        return Admins.Count;
    }
}