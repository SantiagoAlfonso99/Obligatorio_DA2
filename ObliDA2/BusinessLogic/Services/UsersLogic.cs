using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class UsersLogic : IUsersLogic
{
    private IMaintenanceStaffRepository staffRepo;
    private IManagerRepository managerRepo;
    private IAdminRepository adminRepo;

    public UsersLogic(IMaintenanceStaffRepository staffRepoIn, IAdminRepository adminRepoIn, IManagerRepository managerRepoIn)
    {
        staffRepo = staffRepoIn;
        managerRepo = managerRepoIn;
        adminRepo = adminRepoIn;
    }

    public void ValidateEmail(string email)
    {
        bool duplicatedEmail = staffRepo.GetAll().Exists(staff => staff.Email == email);
        duplicatedEmail = duplicatedEmail || managerRepo.GetAll().ToList().Exists(manager => manager.Email == email);
        duplicatedEmail = duplicatedEmail || adminRepo.GetAll().Exists(admin => admin.Email == email);
        if (duplicatedEmail)
        {
            throw new DuplicateEntryException();
        }
    }
}