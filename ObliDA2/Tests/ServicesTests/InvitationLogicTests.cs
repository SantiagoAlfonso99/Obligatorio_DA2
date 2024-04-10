using System.Data.Common;
using Domain.Exceptions;
using Domain.Models;
using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Moq;
namespace Tests.ServicesTests;

[TestClass]
public class InvitationLogicTests
{
    [TestMethod]
    public void GetAllReturnElements()
    {
        Mock<IInvitationRepository> repo = new Mock<IInvitationRepository>();
        List<Invitation> invitations = new List<Invitation>() { new Invitation(){Id = 1}};
        InvitationLogic service = new InvitationLogic(repo.Object);
        repo.Setup(repo => repo.GetAll()).Returns(invitations);

        List<Invitation> returnedList = service.GetAll();
        repo.VerifyAll();
        CollectionAssert.AreEqual(returnedList, invitations);
    }
}