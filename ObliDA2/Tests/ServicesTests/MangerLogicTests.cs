using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Domain.Models;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
namespace Tests.ServicesTests;

[TestClass]
public class ManagerLogicTests
{
    private Mock<IManagerRepository> mockManagerRepo;
    private Mock<IRequestRepository> mockRequestRepo;
    private ManagerLogic managerLogic;

    [TestInitialize]
    public void Setup()
    {
   
        mockManagerRepo = new Mock<IManagerRepository>();
        mockRequestRepo = new Mock<IRequestRepository>();

   
        managerLogic = new ManagerLogic(mockManagerRepo.Object, mockRequestRepo.Object);
    }

    [TestMethod]
    public void ViewRequests_WithNullCategory_ShouldReturnAllRequests()
    {
        var apartmentA = new Apartment { Id = 1, Floor = 3, Number = 101, NumberOfBedrooms = 2, NumberOfBathrooms = 1, Terrace = true, Owner = new ApartmentOwner { Name = "John Doe" }, Building = new Building { Name = "Building A" }};
        var apartmentB = new Apartment { Id = 2, Floor = 2, Number = 102, NumberOfBedrooms = 3, NumberOfBathrooms = 2, Terrace = false, Owner = new ApartmentOwner { Name = "Jane Smith" }, Building = new Building { Name = "Building B" }};
            
        var requests = new List<Request>
        {
            new Request { Description = "Luz Rota", Department = apartmentA, Category = "Electricista" },
            new Request { Description = "Gotera", Department = apartmentB, Category = "Fontanero" }
        };
        mockRequestRepo.Setup(repo => repo.GetAll()).Returns(requests);

   
        var result = managerLogic.ViewRequests();

   
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void ViewRequests_WithCategory_ShouldFilterRequests()
    {
        var apartmentA = new Apartment { Id = 1, Floor = 3, Number = 101, NumberOfBedrooms = 2, NumberOfBathrooms = 1, Terrace = true, Owner = new ApartmentOwner { Name = "John Doe" }, Building = new Building { Name = "Building A" }};
        var apartmentB = new Apartment { Id = 2, Floor = 2, Number = 102, NumberOfBedrooms = 3, NumberOfBathrooms = 2, Terrace = false, Owner = new ApartmentOwner { Name = "Jane Smith" }, Building = new Building { Name = "Building B" }};
            
        var requests = new List<Request>
        {
            new Request { Description = "Luz Rota", Department = apartmentA, Category = "Electricista" },
            new Request { Description = "Gotera", Department = apartmentB, Category = "Fontanero" }
        };
        mockRequestRepo.Setup(repo => repo.FilterByCategory("Fontanero")).Returns(requests.Where(r => r.Category == "Fontanero"));

   
        var result = managerLogic.ViewRequests("Fontanero");

   
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("Fontanero", result.First().Category);
    }

    [TestMethod]
    public void AssignRequestToMaintenance_ValidRequestAndMaintenance_ShouldReturnTrue()
    {
   
        mockRequestRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
        mockManagerRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
        mockRequestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Request());

   
        var result = managerLogic.AssignRequestToMaintenance(1, 1);

   
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidRequestException))]
    public void AssignRequestToMaintenance_InvalidRequestOrMaintenance_ShouldThrowException()
    {
   
        mockRequestRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);
        mockManagerRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);

   
        managerLogic.AssignRequestToMaintenance(1, 1);

   
    }

    [TestMethod]
    public void CreateRequest_ShouldCreateAndReturnRequest()
    {
   
        var description = "New issue";
        var department = new Apartment { Id = 1, Floor = 3, Number = 101, NumberOfBedrooms = 2, NumberOfBathrooms = 1, Terrace = true, Owner = new ApartmentOwner { Name = "John Doe" }, Building = new Building { Name = "Building A" }};
        var category = "Vecino molesto";

   
        var result = managerLogic.CreateRequest(description, department, category);

   
        Assert.IsNotNull(result);
        Assert.AreEqual("Open", result.Status.ToString());
        Assert.AreEqual(description, result.Description);
        Assert.AreEqual(department, result.Department);
        Assert.AreEqual(category, result.Category);
    }
}
