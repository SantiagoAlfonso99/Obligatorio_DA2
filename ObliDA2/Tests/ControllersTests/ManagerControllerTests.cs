using BusinessLogic.Services;
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
        Mock<ICategoryLogic> categoryService = new Mock<ICategoryLogic>();
        Mock<IApartmentLogic> apartmentService = new Mock<IApartmentLogic>();
        Mock<IMaintenanceLogic> staffService = new Mock<IMaintenanceLogic>();
        ManagerController controller = new ManagerController(managerService.Object, categoryService.Object, apartmentService.Object, staffService.Object);
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
    
    [TestMethod]
    public void CreateRequestOk()
    {
        Mock<IManagerLogic> managerService = new Mock<IManagerLogic>();
        Mock<ICategoryLogic> categoryService = new Mock<ICategoryLogic>();
        Mock<IApartmentLogic> apartmentService = new Mock<IApartmentLogic>();
        Mock<IMaintenanceLogic> staffService = new Mock<IMaintenanceLogic>();
        ManagerController controller = new ManagerController(managerService.Object, categoryService.Object, apartmentService.Object, staffService.Object);
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

        Apartment newApartment = new Apartment(){Id = 1, Building = new Building(){Id = 1}};
        Category newCategory = new Category() { Name = "name" };
        Request createdRequest = new Request()
        {
            Id = 1, Department = newApartment, Status = RequestStatus.Open, Category = newCategory,
            Description = "El vecino no para de gritar"
        };
        categoryService.Setup(service => service.GetById(It.IsAny<int>())).Returns(newCategory);
        apartmentService.Setup(service => service.GetById(It.IsAny<int>())).Returns(newApartment);
        managerService.Setup(service =>
                service.CreateRequest(It.IsAny<string>(), It.IsAny<Apartment>(), It.IsAny<Category>()))
            .Returns(createdRequest);
        
        ManagerCreateModel createRequestModel = new ManagerCreateModel() { CategoryId = 1, DepartmentId = 1, Description = "El vecino no para de gritar" };
        var result = controller.CreateRequest(createRequestModel);
        var okResult = result as OkObjectResult;
        ManagerDetailModel returnedRequest = okResult.Value as ManagerDetailModel;
        ManagerDetailModel expectedRequest = new ManagerDetailModel(createdRequest);
        
        Assert.AreEqual(returnedRequest, expectedRequest);
    }
    
    [TestMethod]
    public void AssignRequestOk()
    {
        Mock<IManagerLogic> managerService = new Mock<IManagerLogic>();
        Mock<ICategoryLogic> categoryService = new Mock<ICategoryLogic>();
        Mock<IApartmentLogic> apartmentService = new Mock<IApartmentLogic>();
        Mock<IMaintenanceLogic> staffService = new Mock<IMaintenanceLogic>();
        ManagerController controller = new ManagerController(managerService.Object, categoryService.Object, apartmentService.Object, staffService.Object);
        Apartment newApartment = new Apartment(){Id = 1, Building = new Building(){Id = 1}};
        Category newCategory = new Category() { Name = "name" };
        Request createdRequest = new Request()
        {
            Id = 1, Department = newApartment, Status = RequestStatus.Open, Category = newCategory,
            Description = "El vecino no para de gritar"
        };
        staffService.Setup(service => service.GetById(It.IsAny<int>())).Returns(new MaintenanceStaff(){Id = 1});
        managerService.Setup(service => service.AssignRequestToMaintenance(It.IsAny<int>(), It.IsAny<MaintenanceStaff>())).Returns(true);

        ManagerAssignModel assignRequest = new ManagerAssignModel() { RequestId = 1, MaintenanceId = 1};
        var result = controller.AssignRequest(assignRequest);
        var okResult = result as OkObjectResult;
        var returnedMessage = okResult.Value.GetType().GetProperty("Message"); 
        
        Assert.AreEqual(returnedMessage.GetValue(okResult.Value), "Request successfully assigned.");
    }
    
    [TestMethod]
    public void CannotAssignRequestOk()
    {
        Mock<IManagerLogic> managerService = new Mock<IManagerLogic>();
        Mock<ICategoryLogic> categoryService = new Mock<ICategoryLogic>();
        Mock<IApartmentLogic> apartmentService = new Mock<IApartmentLogic>();
        Mock<IMaintenanceLogic> staffService = new Mock<IMaintenanceLogic>();
        ManagerController controller = new ManagerController(managerService.Object, categoryService.Object, apartmentService.Object, staffService.Object);
        Apartment newApartment = new Apartment(){Id = 1, Building = new Building(){Id = 1}};
        Category newCategory = new Category() { Name = "name" };
        Request createdRequest = new Request()
        {
            Id = 1, Department = newApartment, Status = RequestStatus.Open, Category = newCategory,
            Description = "El vecino no para de gritar"
        };
        staffService.Setup(service => service.GetById(It.IsAny<int>())).Returns(new MaintenanceStaff(){Id = 1});
        managerService.Setup(service => service.AssignRequestToMaintenance(It.IsAny<int>(), It.IsAny<MaintenanceStaff>())).Returns(false);

        ManagerAssignModel assignRequest = new ManagerAssignModel() { RequestId = 1, MaintenanceId = 1};
        var result = controller.AssignRequest(assignRequest);
        var badResult = result as BadRequestObjectResult;
        var returnedMessage = badResult.Value.GetType().GetProperty("Message"); 
        
        Assert.AreEqual(returnedMessage.GetValue(badResult.Value), "Assignment could not be completed");
    }
}