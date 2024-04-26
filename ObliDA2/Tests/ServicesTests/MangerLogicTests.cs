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
   
        var requests = new List<Request>
        {
            new Request { Description = "Light fixture issue", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Electricista"} },
            new Request { Description = "Leaky faucet", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Fontanero"} }
        };
        mockRequestRepo.Setup(repo => repo.GetAll()).Returns(requests);

   
        var result = managerLogic.ViewRequests();

   
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void ViewRequests_WithCategory_ShouldFilterRequests()
    {
   
        var requests = new List<Request>
        {
            new Request { Description = "Light fixture issue", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Electricista"} },
            new Request { Description = "Leaky faucet", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Fontanero"} }
        };
        mockRequestRepo.Setup(repo => repo.GetAll()).Returns(requests);

   
        var result = managerLogic.ViewRequests("Fontanero");
        var expectedList = new List<Request>() { new Request { Description = "Leaky faucet", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Fontanero"}}};
        
        CollectionAssert.AreEqual(expectedList, result.ToList());
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
        var department = new Apartment(){Id =1};
        var category = "Vecino molesto";

   
        var result = managerLogic.CreateRequest(description, department, category);

   
        Assert.IsNotNull(result);
        Assert.AreEqual("Open", result.Status.ToString());
        Assert.AreEqual(description, result.Description);
        Assert.AreEqual(category, result.Category.Name);
    }

    [TestMethod]
    public void CreateManagerOk()
    {
        mockManagerRepo.Setup(repository => repository.Add(It.IsAny<Manager>()));

        Manager newManager = managerLogic.Create(new Manager(){Name = "pepe", Email = "pepe@gmail.com", Password = "Password"});
        Manager expectedManager = new Manager() { Name = "pepe", Email = "pepe@gmail.com", Password = "Password" };

        Assert.AreEqual(expectedManager, newManager);
    }
}
