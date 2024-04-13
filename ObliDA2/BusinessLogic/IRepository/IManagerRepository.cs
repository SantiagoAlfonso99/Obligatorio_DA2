using Domain.Models;
using System.Collections.Generic;

namespace BusinessLogic.IRepository;

public interface IManagerRepository
{
    void Add(Manager manager);
    Manager Get(int managerId);
    bool Exists(int managerId);
    void Update(Manager manager);
    void Remove(Manager manager);
    IEnumerable<Manager> GetAll();
}