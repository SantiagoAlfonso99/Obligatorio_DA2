namespace BusinessLogic;

public class AdminLogic
{
    private List<Admin> Admins;

    public AdminLogic()
    {
        Admins = new List<Admin>();
    }
    public void CreateAdmin(int id)
    {
        Admin newAdmin = new Admin(){Id = id};
        Admins.Add(newAdmin);
    }

    public int AdminsCount()
    {
        return Admins.Count;
    }
}