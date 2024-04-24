using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using IBusinessLogic;
using WebApi.Controllers;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class ManagerControllerTests
{
    private Mock<IManagerLogic> mockManagerLogic;
    private ManagerController controller;

    [TestInitialize]
    public void Setup()
    {
        mockManagerLogic = new Mock<IManagerLogic>();
        controller = new ManagerController(mockManagerLogic.Object);
    }

    [TestMethod]
    public void Create_ShouldReturnCreatedResponse_WhenValidInput()
    {
        var newManager = new Manager { Id = 1, Name = "Juan", LastName = "Doe", Email = "juan.doe@example.com" };
        mockManagerLogic.Setup(x => x.Create(It.IsAny<Manager>())).Returns(newManager);

        var result = controller.Create(new ManagerCreateModel { Name = "Juan", LastName = "Doe", Email = "juan.doe@example.com" }) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        var response = result.Value as ManagerDetailModel;
        Assert.IsNotNull(response);
        Assert.AreEqual(newManager.Name, response.Name);
    }

    [TestMethod]
    public void GetRequests_ShouldReturnAllRequests_WhenCategoryIsNull()
    {
        var requests = new List<Request>
        {
            new Request { Id = 1, Description = "Fix A", Department = "A", Category = "Electricista" },
            new Request { Id = 2, Description = "Fix B", Department = "B", Category = "Fontanero" }
        };
        mockManagerLogic.Setup(x => x.ViewRequests(null)).Returns(requests);

        var result = controller.GetRequests(null) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        var response = result.Value as List<RequestDetailModel>;
        Assert.AreEqual(2, response.Count);
    }

    [TestMethod]
    public void AssignRequest_ShouldReturnOk_WhenValid()
    {
        mockManagerLogic.Setup(x => x.AssignRequestToMaintenance(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

        var result = controller.AssignRequest(new RequestAssignModel { RequestId = 1, MaintenanceId = 2 }) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.IsTrue(result.Value.ToString().Contains("Request successfully assigned."));
    }
}
