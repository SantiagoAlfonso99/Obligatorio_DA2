using BusinessLogic.Models;
namespace BusinessLogic.Services;

public class AdminLogic
{
    private List<Admin> Admins;

    public AdminLogic()
    {
        Admins = new List<Admin>();
    }
    public void CreateAdmin(int id, string name, string password)
    {
        Admin newAdmin = new Admin(){Id = id, Name = name, Password = password};
        Admins.Add(newAdmin);
    }

    public Admin ReturnAdmin(int id)
    {
        return Admins.FirstOrDefault(admin => admin.Id == id);
    }
    
    public int AdminsCount()
    {
        return Admins.Count;
    }
}