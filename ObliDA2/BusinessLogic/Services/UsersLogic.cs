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
    private ISessionRepository sessionRepository;
    private User? currentUser;
    
    public UsersLogic(IMaintenanceStaffRepository staffRepoIn, IAdminRepository adminRepoIn, IManagerRepository managerRepoIn, ISessionRepository sessionRepo)
    {
        staffRepo = staffRepoIn;
        managerRepo = managerRepoIn;
        adminRepo = adminRepoIn;
        sessionRepository = sessionRepo;
    }

    public User? GetCurrentUser(Guid? token = null)
    {
        if (token == null)
        {
            return currentUser;
        }
        currentUser = FindUserByEmail(sessionRepository.FindByToken(token.Value));
        return currentUser;
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
    
    public User FindUserByEmail(string email)
    {
        Manager returnedManager = managerRepo.GetAll().FirstOrDefault(staff => staff.Email == email);
        Admin returnedAdmin = adminRepo.GetAll().FirstOrDefault(staff => staff.Email == email);
        MaintenanceStaff returnedStaff = staffRepo.GetAll().FirstOrDefault(staff => staff.Email == email);
        if (returnedAdmin != null)
        {
            return returnedAdmin;
        }
        else if (returnedStaff != null)
        {
            return returnedStaff;
        }
        else if (returnedManager != null)
        {
            return returnedManager;
        }
        throw new NotFoundException();
    }

    public User LogIn(string email, string password)
    {
        User user = FindUserByEmail(email);
        if (user != null && user.Password == password)
        {
            currentUser = user;
            return user;
        }
        throw new NotFoundException();
    }

    public Guid CreateSession(Session session)
    {
        sessionRepository.Create(session);
        return session.Token;
    }
}