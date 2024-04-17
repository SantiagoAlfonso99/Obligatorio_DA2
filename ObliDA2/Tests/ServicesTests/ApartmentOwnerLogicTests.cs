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
}