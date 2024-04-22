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
    private Mock<IApartmentRepository> repo;
    private Apartment newApartment;
    private Apartment expectedApartment;
    
    [TestInitialize]
    public void Initialize()
    {
        ApartmentOwner owner = new ApartmentOwner() {Id = 1, Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"};
        Building newBuilding = new Building() {Id =1, Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, ConstructionCompany = "Company", BuildingManager = new Manager(){Id = 1}};
        newApartment = new Apartment()
        {
            Id =1, NumberOfBathrooms = 3, NumberOfBedrooms = 4,
            Floor = 3,  Terrace = true, Building = newBuilding,
            Owner = owner, Number = 76
        };
        expectedApartment = new Apartment()
        {
            Id =1, NumberOfBathrooms = 3, NumberOfBedrooms = 4,
            Floor = 3, Terrace = true, Building = newBuilding,
            Owner = owner, Number = 76
        };
        repo = new Mock<IApartmentRepository>();
    }
    
    [TestMethod]
    public void CreateOk()
    {
        repo.Setup(repository => repository.Create(It.IsAny<Apartment>()));
        ApartmentLogic service = new ApartmentLogic(repo.Object);
        
        Apartment returnedApartment = service.Create(newApartment);
        
        repo.VerifyAll();
        Assert.AreEqual(expectedApartment, returnedApartment);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        List<Apartment> consultedElements = new List<Apartment>(){newApartment};
        repo.Setup(repository => repository.GetAll()).Returns(consultedElements);
        ApartmentLogic service = new ApartmentLogic(repo.Object);
        List<Apartment> expectedApartments = new List<Apartment>(){expectedApartment};
        
        List<Apartment> returnedModels = service.GetAll();
        
        repo.VerifyAll();
        CollectionAssert.AreEqual(expectedApartments, returnedModels);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newApartment);
        ApartmentLogic service = new ApartmentLogic(repo.Object);
        
        Apartment returnedModels = service.GetById(1);
        
        repo.VerifyAll();
        Assert.AreEqual(expectedApartment, returnedModels);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetByIdThrowsExceptionForNonExistentId()
    {
        newApartment = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newApartment);
        ApartmentLogic service = new ApartmentLogic(repo.Object);
        
        Apartment returnedModels = service.GetById(1);
        
        repo.VerifyAll();
        Assert.AreEqual(expectedApartment, returnedModels);
    }
    
    [TestMethod]
    public void DeleteReturnsTrue()
    {
        repo.Setup(repository => repository.Delete(It.IsAny<Apartment>()));
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new Apartment(){Id =1});
        ApartmentLogic service = new ApartmentLogic(repo.Object);
        
        bool success = service.Delete(1);
        
        repo.VerifyAll();
        Assert.AreEqual(success, true);
    }
    
    [TestMethod]
    public void DeleteReturnsFalse()
    {
        Apartment nullApartment = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(nullApartment);
        ApartmentLogic service = new ApartmentLogic(repo.Object);
        
        bool success = service.Delete(1);
        
        repo.VerifyAll();
        Assert.AreEqual(success, false);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateApartmentWithNegativeNumberOfBathroomsThrowsException()
    {
        repo.Setup(repository => repository.Create(It.IsAny<Apartment>()));
        ApartmentLogic service = new ApartmentLogic(repo.Object);

        newApartment.NumberOfBathrooms = -1;
        Apartment returnedApartment = service.Create(newApartment);
        
        Assert.AreEqual(expectedApartment, returnedApartment);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateApartmentWithNegativeNumberOfBedroomsThrowsException()
    {
        repo.Setup(repository => repository.Create(It.IsAny<Apartment>()));
        ApartmentLogic service = new ApartmentLogic(repo.Object);

        newApartment.NumberOfBedrooms = -1;
        Apartment returnedApartment = service.Create(newApartment);
        
        Assert.AreEqual(expectedApartment, returnedApartment);
    }
};