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
    public void IndexOkTest()
    {
        List <Invitation> invitations = new List<Invitation>() { new Invitation(){Id = 1}};
        Mock<IInvitationLogic> invitationLogicMock = new Mock<IInvitationLogic>();
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
    
    public void ShowOkTest()
    {
        Invitation expectedInvitation = new Invitation() { Id = 1 };
        Mock<IInvitationLogic> invitationLogicMock = new Mock<IInvitationLogic>();
        invitationLogicMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(expectedInvitation);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        Invitation returnedInvitation = okResult.Value as Invitation;
        
        invitationLogicMock.VerifyAll();
        Assert.AreEqual(
            expectedInvitation,
             returnedInvitation
        );
    }
    
    public void CreateOkTest()
    {
        Invitation expectedInvitation = new Invitation() { Id = 1 };
        Mock<IInvitationLogic> invitationLogicMock = new Mock<IInvitationLogic>();
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
    
    public void DeleteOkTest()
    {
        Mock<IInvitationLogic> invitationLogicMock = new Mock<IInvitationLogic>();
        invitationLogicMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(true);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Delete(1);
        var noContentResult = result as NoContentResult;
        
        invitationLogicMock.VerifyAll();
        
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void DeleteNotFoundTest()
    {
        Mock<IInvitationLogic> invitationLogicMock = new Mock<IInvitationLogic>();
        invitationLogicMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(false);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Delete(1);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty("Message");
        
        invitationLogicMock.VerifyAll();

        Assert.AreEqual("The deletion action could not be completed because there is no invitation with that ID", message.GetValue(notFoundResult.Value));
    }
}