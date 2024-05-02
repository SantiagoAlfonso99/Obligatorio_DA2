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
    private Apartment newApartment;
    private ApartmentDetailModel expectedModel;
    private const int UserId = 1;
    private const string PropertyName = "Message";
    private const string NotFoundMessage = "There is no apartment for that Id";
    
    [TestInitialize]
    public void Initialize()
    {
        service = new Mock<IApartmentLogic>();
        buildingService = new Mock<IBuildingLogic>();
        ownerService = new Mock<IApartmentOwnerLogic>();
        newApartment = new Apartment() { Id = UserId };
        expectedModel = new ApartmentDetailModel(newApartment);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        List<Apartment> apartments = new List<Apartment>(){newApartment};
        service.Setup(logic => logic.GetAll()).Returns(apartments);
        ApartmentController controller = new ApartmentController(service.Object, buildingService.Object, ownerService.Object);
        
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
        ApartmentController controller = new ApartmentController(service.Object, buildingService.Object, ownerService.Object);
        
        var result = controller.Show(UserId);
        var okResult = result as OkObjectResult;
        ApartmentDetailModel returnedApartment = okResult.Value as ApartmentDetailModel;
        
        service.VerifyAll();
        Assert.AreEqual(returnedApartment, expectedModel);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        buildingService.Setup(buildingLogic => buildingLogic.GetById(It.IsAny<int>())).Returns(new Building(){Id = 1});
        ownerService.Setup(ownerLogic => ownerLogic.GetById(It.IsAny<int>())).Returns(new ApartmentOwner() { Id = 1 });
        service.Setup(logic => logic.Create(It.IsAny<Apartment>())).Returns(newApartment);
        ApartmentController controller = new ApartmentController(service.Object, buildingService.Object, ownerService.Object);
        
        var result = controller.Create(new ApartmentCreateModel{ Number = 1});
        var okResult = result as OkObjectResult;
        ApartmentDetailModel returnedApartment = okResult.Value as ApartmentDetailModel;
        
        service.VerifyAll();
        Assert.AreEqual(returnedApartment, expectedModel);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        ApartmentController controller = new ApartmentController(service.Object, buildingService.Object, ownerService.Object);
        
        var result = controller.Delete(UserId);
        var noContentResult = result as NoContentResult;
        
        service.VerifyAll();
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void DeleteReturnsFalse()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(false);
        ApartmentController controller = new ApartmentController(service.Object, buildingService.Object, ownerService.Object);
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        Assert.AreEqual(NotFoundMessage, message.GetValue(notFoundResult.Value));
    }
}