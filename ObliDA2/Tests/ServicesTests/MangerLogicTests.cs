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
        // Initialize the mocks
        mockManagerRepo = new Mock<IManagerRepository>();
        mockRequestRepo = new Mock<IRequestRepository>();

        // Create an instance of ManagerLogic with the mocked repositories
        managerLogic = new ManagerLogic(mockManagerRepo.Object, mockRequestRepo.Object);
    }

    // Test for viewing requests with no category filter
    [TestMethod]
    public void ViewRequests_WithNullCategory_ShouldReturnAllRequests()
    {
        // Arrange
        var requests = new List<Request>
        {
            new Request { Description = "Light fixture issue", Department = "Maintenance", Category = "Electricista" },
            new Request { Description = "Leaky faucet", Department = "Plumbing", Category = "Fontanero" }
        };
        mockRequestRepo.Setup(repo => repo.GetAll()).Returns(requests);

        // Act
        var result = managerLogic.ViewRequests();

        // Assert
        Assert.AreEqual(2, result.Count());
    }

    // Test for viewing requests with a specific category
    [TestMethod]
    public void ViewRequests_WithCategory_ShouldFilterRequests()
    {
        // Arrange
        var requests = new List<Request>
        {
            new Request { Description = "Light fixture issue", Department = "Maintenance", Category = "Electricista" },
            new Request { Description = "Leaky faucet", Department = "Plumbing", Category = "Fontanero" }
        };
        mockRequestRepo.Setup(repo => repo.FilterByCategory("Fontanero")).Returns(requests.Where(r => r.Category == "Fontanero"));

        // Act
        var result = managerLogic.ViewRequests("Fontanero");

        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("Fontanero", result.First().Category);
    }

    // Test assigning a request to maintenance
    [TestMethod]
    public void AssignRequestToMaintenance_ValidRequestAndMaintenance_ShouldReturnTrue()
    {
        // Arrange
        mockRequestRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
        mockManagerRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
        mockRequestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Request());

        // Act
        var result = managerLogic.AssignRequestToMaintenance(1, 1);

        // Assert
        Assert.IsTrue(result);
    }

    // Test assigning a request to an invalid maintenance or request ID
    [TestMethod]
    [ExpectedException(typeof(InvalidRequestException))]
    public void AssignRequestToMaintenance_InvalidRequestOrMaintenance_ShouldThrowException()
    {
        // Arrange
        mockRequestRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);
        mockManagerRepo.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);

        // Act
        managerLogic.AssignRequestToMaintenance(1, 1);

        // No need for assert, expected exception attribute handles the test case
    }

    // Test creating a new request
    [TestMethod]
    public void CreateRequest_ShouldCreateAndReturnRequest()
    {
        // Arrange
        var description = "New issue";
        var department = "IT";
        var category = "Vecino molesto";

        // Act
        var result = managerLogic.CreateRequest(description, department, category);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Open", result.Status.ToString());
        Assert.AreEqual(description, result.Description);
        Assert.AreEqual(department, result.Department);
        Assert.AreEqual(category, result.Category);
    }
}
