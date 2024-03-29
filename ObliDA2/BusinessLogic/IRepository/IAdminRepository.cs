using BusinessLogic.Models;
namespace BusinessLogic.IRepository;

public interface IAdminRepository
{
    void Add(Admin admin);
    Admin Get(int id);
    void Remove(Admin admin);
    bool Exists(int id);
    int Count();
    void Update(Admin admin);
}