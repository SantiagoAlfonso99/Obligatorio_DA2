using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IConstructionCompanyRepository
{
    void Create(ConstructionCompany newCompany);
    ConstructionCompany GetById(int id);
}