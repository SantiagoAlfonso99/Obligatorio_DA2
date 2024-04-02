using BusinessLogic.IRepository;
using BusinessLogic.Models;
namespace BusinessLogic.Services;

public class AdminLogic
{
    private IAdminRepository adminRepo;

    public AdminLogic(IAdminRepository adminRepoIn)
    {
        adminRepo = adminRepoIn;
    }
    public Admin Create(Admin adminIn)
    {
        adminRepo.Add(adminIn);
        return adminIn;
    }

    public Admin GetById(int id)
    {
        return adminRepo.Get(id);
    }
    
    public int Count()
    {
        return adminRepo.Count();
    }
}