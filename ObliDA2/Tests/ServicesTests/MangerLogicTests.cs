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
        mockRequestRepo.Setup(repo => repo.FilterByCategory("Fontanero")).Returns(requests.Where(r => r.Category.Name == "Fontanero"));

   
        var result = managerLogic.ViewRequests("Fontanero");

        
        Assert.AreEqual("Fontanero", result.First().Category.Name);
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
}
