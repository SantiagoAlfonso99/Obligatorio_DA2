using BusinessLogic.IRepository;
using Domain.Models;
using BusinessLogic.Services;
using IBusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Moq;

namespace Tests.ServicesTests;

[TestClass]
public class ApartmentOwnerLogicTests
{
    [TestMethod]
    public void CreateOk()
    {
        Mock<IApartmentOwnerRepository> repo = new Mock<IApartmentOwnerRepository>();
        repo.Setup(repository => repository.Create(It.IsAny<ApartmentOwner>()));
        ApartmentOwnerLogic service = new ApartmentOwnerLogic(repo.Object);

        ApartmentOwner returnedOwner = service.Create(new ApartmentOwner() { Name = "pepe" });
        ApartmentOwner expectedOwner = new ApartmentOwner() { Name = "pepe" };
        
        repo.VerifyAll();
        Assert.AreEqual(returnedOwner, expectedOwner);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        Mock<IApartmentOwnerRepository> repo = new Mock<IApartmentOwnerRepository>();
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new ApartmentOwner(){Id = 1});
        ApartmentOwnerLogic service = new ApartmentOwnerLogic(repo.Object);

        ApartmentOwner returnedOwner = service.GetById(1);
        ApartmentOwner expectedOwner = new ApartmentOwner() { Id = 1};
        
        repo.VerifyAll();
        Assert.AreEqual(returnedOwner, expectedOwner);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<IApartmentOwnerRepository> repo = new Mock<IApartmentOwnerRepository>();
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new ApartmentOwner(){Id = 1});
        repo.Setup(repository => repository.Delete(It.IsAny<ApartmentOwner>()));
        ApartmentOwnerLogic service = new ApartmentOwnerLogic(repo.Object);

        bool success = service.Delete(1);
        
        repo.VerifyAll();
        Assert.AreEqual(true, success);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        Mock<IApartmentOwnerRepository> repo = new Mock<IApartmentOwnerRepository>();
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new ApartmentOwner(){Id = 1, Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"});
        repo.Setup(repository => repository.Update(It.IsAny<ApartmentOwner>()));
        ApartmentOwnerLogic service = new ApartmentOwnerLogic(repo.Object);

        ApartmentOwner returnedOwner = service.Update(1, new ApartmentOwner(){Name = "Pepe", LastName = "rodriguez", Email = "pepe@gmail.com"});
        ApartmentOwner expectedOwner = new ApartmentOwner()
            { Id = 1, Name = "Pepe", LastName = "rodriguez", Email = "pepe@gmail.com" };
        repo.VerifyAll();
        Assert.AreEqual(expectedOwner, returnedOwner);
    }
}