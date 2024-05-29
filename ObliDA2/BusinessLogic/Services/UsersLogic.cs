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
    private ICompanyAdminRepository companyAdminRepository;
    private User? currentUser;
    
    public UsersLogic(IMaintenanceStaffRepository staffRepoIn, IAdminRepository adminRepoIn, IManagerRepository managerRepoIn, ISessionRepository sessionRepo
    , ICompanyAdminRepository companyAdminIn)
    {
        staffRepo = staffRepoIn;
        managerRepo = managerRepoIn;
        adminRepo = adminRepoIn;
        sessionRepository = sessionRepo;
        companyAdminRepository = companyAdminIn;
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
        try
        {
            User returnedUser = FindUserByEmail(email);
            throw new DuplicateEntryException();
        }
        catch(NotFoundException)
        {
            return;
        }
    }
    
    public User FindUserByEmail(string email)
    {
        List<User> users = new List<User>();
        users.AddRange(managerRepo.GetAll().ToList());
        users.AddRange(adminRepo.GetAll());
        users.AddRange(staffRepo.GetAll());
        users.AddRange(companyAdminRepository.GetAll());
        User returnedUser = users.FirstOrDefault(user => user.Email == email);
        if (returnedUser != null)
        {
            return returnedUser;
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