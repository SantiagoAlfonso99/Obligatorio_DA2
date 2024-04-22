using BusinessLogic.IRepository;
using Domain.Models;
using BusinessLogic.Services;
using Domain.Exceptions;
using IBusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Moq;

namespace Tests.ServicesTests;

[TestClass]
public class ApartmentLogicTests
{
    [TestMethod]
    public void CreateOk()
    {
        Mock<IApartmentRepository> repo = new Mock<IApartmentRepository>();
        repo.Setup(repository => repository.Create(It.IsAny<Apartment>()));
        ApartmentLogic service = new ApartmentLogic(repo.Object);

        
        Apartment returnedApartment = service.Create(new Apartment(){Id = 1});
        Apartment expectedApartment = new Apartment() { Id = 1 };
        Assert.AreEqual(expectedApartment, returnedApartment);
    }
    
    
}