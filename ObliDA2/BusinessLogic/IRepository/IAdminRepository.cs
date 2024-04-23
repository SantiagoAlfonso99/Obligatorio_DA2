using Domain.Models;
namespace BusinessLogic.IRepository;

public interface IAdminRepository
{
    List<Admin> GetAll();
    void Add(Admin admin);
    Admin Get(int id);
    void Remove(Admin admin);
    void Update(Admin admin);
}