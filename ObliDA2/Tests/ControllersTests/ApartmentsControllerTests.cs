using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.Out;
using WebApi.DTOs.In;

namespace Tests.ControllersTests;
[TestClass]
public class ApartmentsControllerTests
{
    private Mock<IApartmentLogic> service;
    private Mock<IBuildingLogic> buildingService;
    private Mock<IApartmentOwnerLogic> ownerService;
    private Mock<IUsersLogic> usersLogic;
    private Apartment newApartment;
    private ApartmentDetailModel expectedModel;
    private const int UserId = 1;
    private const string PropertyName = "Message";
    private const string BadRequestMessage = "You do not have access to the specified building.";
    private ApartmentController controller;
    
    [TestInitialize]
    public void Initialize()
    {
        usersLogic = new Mock<IUsersLogic>();
        service = new Mock<IApartmentLogic>();
        buildingService = new Mock<IBuildingLogic>();
        ownerService = new Mock<IApartmentOwnerLogic>();
        newApartment = new Apartment() { Id = UserId };
        expectedModel = new ApartmentDetailModel(newApartment);
        controller = new ApartmentController(service.Object, buildingService.Object, ownerService.Object, usersLogic.Object);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        List<Apartment> apartments = new List<Apartment>(){newApartment};
        service.Setup(logic => logic.GetAll()).Returns(apartments);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<ApartmentDetailModel> returnedApartments = okResult.Value as List<ApartmentDetailModel>;
        List<ApartmentDetailModel> expectedList = new List<ApartmentDetailModel>(){expectedModel};
        
        service.VerifyAll();
        CollectionAssert.AreEqual(returnedApartments, expectedList);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newApartment);
        
        var result = controller.Show(UserId);
        var okResult = result as OkObjectResult;
        ApartmentDetailModel returnedApartment = okResult.Value as ApartmentDetailModel;
        
        service.VerifyAll();
        Assert.AreEqual(returnedApartment, expectedModel);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        var currentUser = new CompanyAdmin() { Name = "Name2", Email = "email2@gmail.com", Password = "password",
            Id = 3, CompanyId = 1};
        usersLogic.Setup(service => service.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        buildingService.Setup(buildingLogic => buildingLogic.GetById(It.IsAny<int>())).Returns(new Building(){Id = 1, CompanyId = 1});
        ownerService.Setup(ownerLogic => ownerLogic.GetById(It.IsAny<int>())).Returns(new ApartmentOwner() { Id = 1 });
        service.Setup(logic => logic.Create(It.IsAny<Apartment>())).Returns(newApartment);
        
        var result = controller.Create(new ApartmentCreateModel{ Number = 1});
        var okResult = result as OkObjectResult;
        ApartmentDetailModel returnedApartment = okResult.Value as ApartmentDetailModel;
        
        service.VerifyAll();
        Assert.AreEqual(returnedApartment, expectedModel);
    }
    
    [TestMethod]
    public void CreateReturnsBadRequest()
    {
        var currentUser = new CompanyAdmin() { Name = "Name2", Email = "email2@gmail.com", Password = "password",
            Id = 3, CompanyId = 1};
        usersLogic.Setup(service => service.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        buildingService.Setup(buildingLogic => buildingLogic.GetById(It.IsAny<int>())).Returns(new Building(){Id = 1, CompanyId = 2});
        ownerService.Setup(ownerLogic => ownerLogic.GetById(It.IsAny<int>())).Returns(new ApartmentOwner() { Id = 1 });
        
        var result = controller.Create(new ApartmentCreateModel{ Number = 1});
        var badResult = result as BadRequestObjectResult;
        var message = badResult.Value.GetType().GetProperty(PropertyName);
        
        usersLogic.VerifyAll();
        buildingService.VerifyAll();
        service.VerifyAll();
        Assert.AreEqual(BadRequestMessage, message.GetValue(badResult.Value));
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        var currentUser = new Manager() { Name = "Name2", Email = "email2@gmail.com", Password = "password",
            Id = 3};
        usersLogic.Setup(userService => userService.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        buildingService.Setup(buildingLogic => buildingLogic.GetById(It.IsAny<int>())).Returns(new Building(){Id = 1, BuildingManagerId = 3});
        newApartment.BuildingId = 1;
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newApartment);
        service.Setup(logic => logic.Delete(It.IsAny<Apartment>())).Returns(newApartment);
        
        var result = controller.Delete(UserId);
        var noContentResult = result as NoContentResult;
        
        usersLogic.VerifyAll();
        buildingService.VerifyAll();
        service.VerifyAll();
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void DeleteReturnsBadRequest()
    {
        var currentUser = new Manager() { Name = "Name2", Email = "email2@gmail.com", Password = "password",
            Id = 3};
        usersLogic.Setup(userService => userService.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        buildingService.Setup(buildingLogic => buildingLogic.GetById(It.IsAny<int>())).Returns(new Building(){Id = 1, BuildingManagerId = 1});
        newApartment.BuildingId = 1;
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newApartment);
        
        var result = controller.Delete(UserId);
        var badResult = result as BadRequestObjectResult;
        var message = badResult.Value.GetType().GetProperty(PropertyName);
        
        usersLogic.VerifyAll();
        buildingService.VerifyAll();
        service.VerifyAll();
        Assert.AreEqual(BadRequestMessage, message.GetValue(badResult.Value));
    }
}