using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace Tests.ControllersTests;

[TestClass]
public class ManagerControllerTests
{

    [TestMethod]
    public void GetRequestOk()
    {
        Mock<IManagerLogic> managerService = new Mock<IManagerLogic>();
        ManagerController controller = new ManagerController(managerService.Object);
        Request newRequest = new Request()
        {
            AssignedToMaintenance = new MaintenanceStaff(){Id =1},Category = new Category(){Name = "Name"},
            BuildingAssociated  = new Building(){Id = 1}, Department = new Apartment(){Id = 1}, Description = "p"
        };
        Request otherRequest = new Request()
        {
            AssignedToMaintenance = new MaintenanceStaff(){Id =1},Category = new Category(){Name = "Name"},
            BuildingAssociated  = new Building(){Id = 1}, Department = new Apartment(){Id = 1}, Description = "pepe"
        };
        List<Request> requests = new List<Request>() { newRequest, otherRequest};
        managerService.Setup(managerService => managerService.ViewRequests("Name")).Returns(requests);
        
        var result = controller.GetRequests("Name");
        var okResult = result as OkObjectResult;
        List<ManagerDetailModel> returnedRequests = okResult.Value as List<ManagerDetailModel>;
        List<ManagerDetailModel> expectedModels = requests.Select(request => new ManagerDetailModel(request)).ToList();
        
        CollectionAssert.AreEqual(expectedModels, returnedRequests);
    }
}