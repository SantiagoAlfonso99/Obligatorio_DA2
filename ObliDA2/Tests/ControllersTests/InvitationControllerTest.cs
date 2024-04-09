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
public class InvitationControllerTest
{
    private Invitation expectedInvitation;
    private Mock<IInvitationLogic> invitationLogicMock;
    private const int UserId = 1;
    private const string EmailResponse = "pepe@gmail.com";
    private const string Password = "pepe";
    private const string InvitationNotFound = "Error, invitation not found.";
    private const string AcceptInvitationBadRequest = "Please ensure to enter a non-null or non-empty email and password";
    private const string CreateInvitationBadRequest = "Make sure not to insert null values and that there is no other invitation for the same recipient";
    private const string ShowInvitationNotFoundMes = "The show action could not be completed because there is no invitation with that ID";
    private const string PropertyName = "Message";
    private const string Name = "pepe";
    private const string ExpectedMessage = "The deletion action could not be completed because there is no invitation with that ID";
    private const bool ReturnsFalse = false;
    private const bool ReturnsTrue = true;
    private InvitationResponse response;
    
    [TestInitialize]
    public void Initialize()
    {
        expectedInvitation = new Invitation(){Id = UserId, Name = Name, CreatorId = 2, DeadLine = DateTime.Now, Email = EmailResponse};
        invitationLogicMock = new Mock<IInvitationLogic>();
        response = new InvitationResponse()
            { Email = EmailResponse, acceptInvitation = true, Password = Password };
    }
    
    [TestMethod]
    public void IndexOkTest()
    {
        List <Invitation> invitations = new List<Invitation>() { expectedInvitation};
        invitationLogicMock.Setup(r => r.GetAll()).Returns(invitations);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<InvitationDetailModel> returnedInvitations = okResult.Value as List<InvitationDetailModel>;
        List<InvitationDetailModel> expectedList =
            invitations.Select(invitation => new InvitationDetailModel(invitation)).ToList();
            
        invitationLogicMock.VerifyAll();
        CollectionAssert.AreEqual(
            expectedList,
            returnedInvitations
        );
    }
    
    [TestMethod]
    public void ShowOkTest()
    {
        invitationLogicMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(expectedInvitation);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Show(UserId);
        var okResult = result as OkObjectResult;
        InvitationDetailModel returnedInvitation = okResult.Value as InvitationDetailModel;
        
        invitationLogicMock.VerifyAll();
        Assert.AreEqual(
            new InvitationDetailModel(expectedInvitation),
             returnedInvitation
        );
    }
    
    [TestMethod]
    public void CreateOkTest()
    {
        InvitationCreateModel newInvitation = new InvitationCreateModel() { CreatorId = 2};
        invitationLogicMock.Setup(r => r.Create(It.IsAny<Invitation>())).Returns(expectedInvitation);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Create(newInvitation);
        var okResult = result as OkObjectResult;
        InvitationDetailModel returnedInvitation = okResult.Value as InvitationDetailModel;
    
        invitationLogicMock.VerifyAll();
        Assert.AreEqual(
            new InvitationDetailModel(expectedInvitation),
            returnedInvitation
        );
    }
    
    [TestMethod]
    public void DeleteOkTest()
    {
        invitationLogicMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(ReturnsTrue);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Delete(UserId);
        var noContentResult = result as NoContentResult;
        
        invitationLogicMock.VerifyAll();
        
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void DeleteNotFoundTest()
    {
        invitationLogicMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(ReturnsFalse);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        invitationLogicMock.VerifyAll();

        Assert.AreEqual(ExpectedMessage, message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void UserAcceptInvitationOkTest()
    {
        invitationLogicMock.Setup(r => r.InvitationResponse(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(expectedInvitation);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.InvitationResponse(UserId, response);
        var okResult = result as OkObjectResult;
        InvitationDetailModel returnedInvitation = okResult.Value as InvitationDetailModel;
        
        invitationLogicMock.VerifyAll();

        Assert.AreEqual(new InvitationDetailModel(expectedInvitation), returnedInvitation);
    }
    
    [TestMethod]
    public void UserAcceptInvitationThrowExceptionTest()
    {
        invitationLogicMock.Setup(r => r.InvitationResponse(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Throws(new InvalidInvitationException());
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.InvitationResponse(UserId, response);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        invitationLogicMock.VerifyAll();

        Assert.AreEqual(InvitationNotFound, message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void UserAcceptInvitationBadRequestTest()
    {
        InvitationResponse response = new InvitationResponse()
            { Email = "", acceptInvitation = true, Password = "" };
        invitationLogicMock.Setup(r => r.InvitationResponse(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(expectedInvitation);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.InvitationResponse(UserId, response);
        var badRequestResult = result as BadRequestObjectResult;
        var message = badRequestResult.Value.GetType().GetProperty(PropertyName);

        Assert.AreEqual(AcceptInvitationBadRequest, message.GetValue(badRequestResult.Value));
    }
    
    [TestMethod]
    public void CreateInvitationBadRequestTest()
    {
        InvitationCreateModel newInvitation = new InvitationCreateModel() { CreatorId = 2};
        invitationLogicMock.Setup(r => r.Create(It.IsAny<Invitation>())).Throws(new InvalidInvitationException());
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Create(newInvitation);
        var badRequestResult = result as BadRequestObjectResult;
        var message = badRequestResult.Value.GetType().GetProperty(PropertyName);

        Assert.AreEqual(CreateInvitationBadRequest, message.GetValue(badRequestResult.Value));
    }
    
    [TestMethod]
    public void ShowInvitationNotFoundTest()
    {
        invitationLogicMock.Setup(r => r.GetById(It.IsAny<int>())).Throws(new InvalidInvitationException());
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Show(UserId);
        var badRequestResult = result as NotFoundObjectResult;
        var message = badRequestResult.Value.GetType().GetProperty(PropertyName);

        Assert.AreEqual(ShowInvitationNotFoundMes, message.GetValue(badRequestResult.Value));
    }
}