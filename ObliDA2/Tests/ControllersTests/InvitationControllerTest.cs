using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;

namespace Tests.ControllersTests;

[TestClass]
public class InvitationControllerTest
{
    private Invitation expectedInvitation;
    private Mock<IInvitationLogic> invitationLogicMock;
    private const int UserId = 1;
    private const string PropertyName = "Message";
    private const string ExpectedMessage =
        "The deletion action could not be completed because there is no invitation with that ID";
    private const bool ReturnsFalse = false;
    private const bool ReturnsTrue = true;
    
    [TestInitialize]
    public void Initialize()
    {
        expectedInvitation = new Invitation(){Id = UserId};
        invitationLogicMock = new Mock<IInvitationLogic>();
    }
    
    [TestMethod]
    public void IndexOkTest()
    {
        List <Invitation> invitations = new List<Invitation>() { expectedInvitation};
        invitationLogicMock.Setup(r => r.GetAll()).Returns(invitations);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<Invitation> returnedInvitations = okResult.Value as List<Invitation>;
        
        invitationLogicMock.VerifyAll();
        CollectionAssert.AreEqual(
            invitations,
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
        Invitation returnedInvitation = okResult.Value as Invitation;
        
        invitationLogicMock.VerifyAll();
        Assert.AreEqual(
            expectedInvitation,
             returnedInvitation
        );
    }
    
    [TestMethod]
    public void CreateOkTest()
    {
        invitationLogicMock.Setup(r => r.Create(It.IsAny<Invitation>())).Returns(expectedInvitation);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Create(expectedInvitation);
        var okResult = result as OkObjectResult;
        Invitation returnedInvitation = okResult.Value as Invitation;
        
        invitationLogicMock.VerifyAll();
        Assert.AreEqual(
            expectedInvitation,
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
        
        var result = controller.InvitationResponse(UserId, "pepe@gmail.com", "pepe", true);
        var okResult = result as OkObjectResult;
        Invitation returnedInvitation = okResult.Value as Invitation;
        
        invitationLogicMock.VerifyAll();

        Assert.AreEqual(expectedInvitation, returnedInvitation);
    }
}