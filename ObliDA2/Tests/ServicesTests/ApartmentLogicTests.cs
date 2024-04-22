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
    
    [TestMethod]
    public void GetAllOk()
    {
        Mock<IApartmentRepository> repo = new Mock<IApartmentRepository>();
        List<Apartment> consultedElements = new List<Apartment>(){new Apartment(){Id = 1}};
        repo.Setup(repository => repository.GetAll()).Returns(consultedElements);
        ApartmentLogic service = new ApartmentLogic(repo.Object);

        
        List<Apartment> returnedModels = service.GetAll();
        List<Apartment> expectedApartments = new List<Apartment>(){new Apartment(){Id = 1}};
        CollectionAssert.AreEqual(expectedApartments, returnedModels);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        Mock<IApartmentRepository> repo = new Mock<IApartmentRepository>();
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new Apartment(){Id = 1});
        ApartmentLogic service = new ApartmentLogic(repo.Object);

        
        Apartment returnedModels = service.GetById(1);
        Apartment expectedApartment = new Apartment(){Id = 1};
        Assert.AreEqual(expectedApartment, returnedModels);
    }
    
    [TestMethod]
    public void DeleteReturnsTrue()
    {
        Mock<IApartmentRepository> repo = new Mock<IApartmentRepository>();
        repo.Setup(repository => repository.Delete(It.IsAny<Apartment>()));
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new Apartment(){Id =1});
        ApartmentLogic service = new ApartmentLogic(repo.Object);

        
        bool success = service.Delete(1);
        Assert.AreEqual(success, true);
    }
    
    [TestMethod]
    public void DeleteReturnsFalse()
    {
        Apartment nullApartment = null;
        Mock<IApartmentRepository> repo = new Mock<IApartmentRepository>();
        repo.Setup(repository => repository.Delete(It.IsAny<Apartment>()));
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(nullApartment);
        ApartmentLogic service = new ApartmentLogic(repo.Object);

        
        bool success = service.Delete(1);
        Assert.AreEqual(success, false);
    }
};