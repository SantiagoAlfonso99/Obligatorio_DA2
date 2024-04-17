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
    private Mock<IApartmentOwnerRepository> repo;
    private ApartmentOwner newOwner;
    private ApartmentOwner expectedOwner;
    private const int UserId = 1;
    
    [TestInitialize]
    public void Initialize()
    {
        repo = new Mock<IApartmentOwnerRepository>();
        newOwner = new ApartmentOwner() {Id = 1, Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"};
        expectedOwner = new ApartmentOwner()
            { Id = 1, Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com" };
    }
    
    [TestMethod]
    public void CreateOk()
    {
        repo.Setup(repository => repository.Create(It.IsAny<ApartmentOwner>()));
        ApartmentOwnerLogic service = new ApartmentOwnerLogic(repo.Object);

        ApartmentOwner returnedOwner = service.Create(newOwner);
        
        repo.VerifyAll();
        Assert.AreEqual(returnedOwner, expectedOwner);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newOwner);
        ApartmentOwnerLogic service = new ApartmentOwnerLogic(repo.Object);

        ApartmentOwner returnedOwner = service.GetById(UserId);
        
        repo.VerifyAll();
        Assert.AreEqual(returnedOwner, expectedOwner);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newOwner);
        repo.Setup(repository => repository.Delete(It.IsAny<ApartmentOwner>()));
        ApartmentOwnerLogic service = new ApartmentOwnerLogic(repo.Object);

        bool success = service.Delete(UserId);
        
        repo.VerifyAll();
        Assert.AreEqual(true, success);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new ApartmentOwner(){Id = 1, Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"});
        repo.Setup(repository => repository.Update(It.IsAny<ApartmentOwner>()));
        ApartmentOwnerLogic service = new ApartmentOwnerLogic(repo.Object);

        ApartmentOwner returnedOwner = service.Update(UserId, new ApartmentOwner(){Name = "Pepe", LastName = "rodriguez", Email = "pepe@gmail.com"});
        expectedOwner.Name = "Pepe";
        expectedOwner.LastName = "rodriguez";
        expectedOwner.Email = "pepe@gmail.com";
        
        repo.VerifyAll();
        Assert.AreEqual(expectedOwner, returnedOwner);
    }
}