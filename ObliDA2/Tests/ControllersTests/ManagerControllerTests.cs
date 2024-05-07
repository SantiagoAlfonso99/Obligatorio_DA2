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
    private Mock<IManagerLogic> managerService;
    private Mock<ICategoryLogic> categoryService;
    private Mock<IApartmentLogic> apartmentService;
    private Mock<IMaintenanceLogic> staffService;
    private Mock<IUsersLogic> userLogic;
    private ManagerController controller;
    private MaintenanceStaff returnedMaintenance;
    private DateTime timeNow;
    private Apartment newApartment;
    private Category newCategory;
    
    [TestInitialize]
    public void Initialize()
    {
        returnedMaintenance = new MaintenanceStaff()
            { Name = "pepe", LastName = "rodriguez", Password = "juan123", Email = "pepe@gmail.com", Id = 1 };
        timeNow = DateTime.Now;
        newApartment = new Apartment(){Id = 1, Building = new Building(){Id = 1}};
        newCategory = new Category() { Name = "name" };
        
        userLogic = new Mock<IUsersLogic>();
        managerService = new Mock<IManagerLogic>();
        categoryService = new Mock<ICategoryLogic>();
        apartmentService = new Mock<IApartmentLogic>();
        staffService = new Mock<IMaintenanceLogic>();
        controller = new ManagerController(managerService.Object, categoryService.Object, apartmentService.Object, staffService.Object, userLogic.Object);
    }
    
    [TestMethod]
    public void GetRequestOk()
    {
        Request newRequest = new Request()
        {
            AssignedToMaintenance = new MaintenanceStaff(){Id =1},Category = new Category(){Name = "Name"},
            Department = new Apartment(){Id = 1, BuildingId = 1}, Description = "p"
        };
        Request otherRequest = new Request()
        {
            AssignedToMaintenance = new MaintenanceStaff(){Id =1},Category = new Category(){Name = "Name"},
            Department = new Apartment(){Id = 1, BuildingId = 1}, Description = "pepe"
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
        staffService.Setup(service => service.GetById(It.IsAny<int>())).Returns(new MaintenanceStaff(){Id = 1});
        managerService.Setup(service => service.AssignRequestToMaintenance(It.IsAny<int>(), It.IsAny<MaintenanceStaff>())).Returns(false);

        ManagerAssignModel assignRequest = new ManagerAssignModel() { RequestId = 1, MaintenanceId = 1};
        var result = controller.AssignRequest(assignRequest);
        var badResult = result as BadRequestObjectResult;
        var returnedMessage = badResult.Value.GetType().GetProperty("Message"); 
        
        Assert.AreEqual(returnedMessage.GetValue(badResult.Value), "Assignment could not be completed");
    }

    [TestMethod]
    public void MaintenanceStaffAcceptRequestOk()
    {
        userLogic.Setup(service => service.GetCurrentUser(It.IsAny<Guid?>())).Returns(returnedMaintenance);
        Request createdRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Open, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, AssignedToMaintenance = returnedMaintenance
        };
        managerService.Setup(service => service.GetAllRequest()).Returns(new List<Request> { createdRequest });
        Request returnedRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Attending, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, AssignedToMaintenance = returnedMaintenance,
            Service_start = timeNow
        };
        managerService.Setup(managerService => managerService.MaintenanceStaffAcceptRequest(It.IsAny<Request>(), It.IsAny<DateTime>()))
            .Returns(returnedRequest);
        MaintenanceStaffResponse expectedModel = new MaintenanceStaffResponse(returnedRequest);
        
        AcceptRequestDTO acceptRequest = new AcceptRequestDTO() { RequestId = 3 };
        var result = controller.AcceptRequest(acceptRequest);
        userLogic.VerifyAll();
        var okResult = result as OkObjectResult;
        MaintenanceStaffResponse returnedValue = okResult.Value as MaintenanceStaffResponse;
        
        Assert.AreEqual(expectedModel, returnedValue);
    }
    
    [TestMethod]
    public void MaintenanceStaffCompleteRequestOk()
    {
       userLogic.Setup(service => service.GetCurrentUser(It.IsAny<Guid?>())).Returns(returnedMaintenance);
       Request createdRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Attending, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, AssignedToMaintenance = returnedMaintenance,
            Service_start = timeNow.AddDays(-1)
        };
        managerService.Setup(service => service.GetAllRequest()).Returns(new List<Request> { createdRequest });
        Request returnedRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Closed, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, AssignedToMaintenance = returnedMaintenance,
            Service_start = timeNow.AddDays(-1), Service_end = timeNow, FinalCost = 500
        };
        managerService.Setup(managerService => managerService.MaintenanceStaffCompleteRequest(It.IsAny<Request>(), It.IsAny<int>(), It.IsAny<DateTime>()))
            .Returns(returnedRequest);
        MaintenanceStaffResponse expectedModel = new MaintenanceStaffResponse(returnedRequest);
        
        CompleteRequestDTO acceptRequest = new CompleteRequestDTO() { RequestId = 3, FinalPrice = 500};
        var result = controller.CompleteRequest(acceptRequest);
        userLogic.VerifyAll();
        var okResult = result as OkObjectResult;
        MaintenanceStaffResponse returnedValue = okResult.Value as MaintenanceStaffResponse;
        
        Assert.AreEqual(expectedModel, returnedValue);
    }
    
    [TestMethod]
    public void MaintenanceStaffAcceptRequestThrowsNotFound()
    {
        returnedMaintenance = null;
        userLogic.Setup(service => service.GetCurrentUser(It.IsAny<Guid?>())).Returns(returnedMaintenance);
        Request createdRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Open, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, AssignedToMaintenance = new MaintenanceStaff{Id =1}
        };
        managerService.Setup(service => service.GetAllRequest()).Returns(new List<Request> { createdRequest });
        
        AcceptRequestDTO acceptRequest = new AcceptRequestDTO() { RequestId = 3 };
        var result = controller.AcceptRequest(acceptRequest);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty("Message");
    
        Assert.AreEqual("The user referred to by the token or the given request was not found.", message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void MaintenanceStaffCompleteInvitationThrowsNotFound()
    {
        MaintenanceStaff returnedMaintenance = null;
        Guid? token = null;
        userLogic.Setup(service => service.GetCurrentUser(token)).Returns(returnedMaintenance);
        Request createdRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Open, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, AssignedToMaintenance = returnedMaintenance
        };
        managerService.Setup(service => service.GetAllRequest()).Returns(new List<Request> { createdRequest });
        
        CompleteRequestDTO completeRequest = new CompleteRequestDTO() { RequestId = 3 };
        var result = controller.CompleteRequest(completeRequest);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty("Message");
    
        Assert.AreEqual("The user referred to by the token or the given request was not found.", message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void MaintenanceStaffAcceptInvitationBadRequest()
    {
        Guid? token = null;
        userLogic.Setup(service => service.GetCurrentUser(token)).Returns(returnedMaintenance);
        Request createdRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Attending, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 2
        };
        managerService.Setup(service => service.GetAllRequest()).Returns(new List<Request> { createdRequest });
        
        AcceptRequestDTO acceptRequest = new AcceptRequestDTO() { RequestId = 3 };
        var result = controller.AcceptRequest(acceptRequest);
        var badRequestResult = result as BadRequestObjectResult;
        var message = badRequestResult.Value.GetType().GetProperty("Message");
    
        Assert.AreEqual("Please verify that this is a request from you and that it is still an open request.", message.GetValue(badRequestResult.Value));
    }
    
    [TestMethod]
    public void MaintenanceStaffCompleteInvitationBadRequest()
    {
        Guid? token = null;
        userLogic.Setup(service => service.GetCurrentUser(token)).Returns(returnedMaintenance);
        Request createdRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Open, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 2
        };
        managerService.Setup(service => service.GetAllRequest()).Returns(new List<Request> { createdRequest });
        
        CompleteRequestDTO completeRequest = new CompleteRequestDTO() { RequestId = 3, FinalPrice = 500 };
        var result = controller.CompleteRequest(completeRequest);
        var badRequestResult = result as BadRequestObjectResult;
        var message = badRequestResult.Value.GetType().GetProperty("Message");
    
        Assert.AreEqual("Please verify that this is a request from you and that it is still an open request.", message.GetValue(badRequestResult.Value));
    }
}