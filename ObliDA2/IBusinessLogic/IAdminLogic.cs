using Domain.Models;
namespace IBusinessLogic;

public interface IAdminLogic
{
    List<Admin> GetAll();
    Admin GetById(int id);
}