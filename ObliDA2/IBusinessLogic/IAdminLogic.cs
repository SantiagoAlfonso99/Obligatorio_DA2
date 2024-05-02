using Domain.Models;
namespace IBusinessLogic;

public interface IAdminLogic
{
    List<Admin> GetAll();
    Admin GetById(int id);
    Admin Create(Admin admin);
    Admin Update(int id, Admin admin);
    bool Delete(int id);
}