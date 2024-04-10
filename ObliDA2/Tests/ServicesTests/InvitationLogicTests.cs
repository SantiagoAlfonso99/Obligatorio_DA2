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
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(invitations);
        InvitationLogic service = new InvitationLogic(repo.Object);

        List<Invitation> returnedList = service.GetAll();
        repo.VerifyAll();
        CollectionAssert.AreEqual(returnedList, invitations);
    }
    
    [TestMethod]
    public void GetByIdReturnElement()
    {
        Mock<IInvitationRepository> repo = new Mock<IInvitationRepository>();
        Invitation invitation = new Invitation() { Id = 1 };
        repo.Setup(invitationRepo => invitationRepo.GetById(It.IsAny<int>())).Returns(invitation);
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.GetById(1);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, invitation);
    }
    
    [TestMethod]
    public void CreateInvitationOk()
    {
        Mock<IInvitationRepository> repo = new Mock<IInvitationRepository>();
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com"};
        Invitation expectedInvitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = "Pending"};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation.Id, expectedInvitation.Id);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithEmptyEmailThrowsException()
    {
        Mock<IInvitationRepository> repo = new Mock<IInvitationRepository>();
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = ""};
        Invitation expectedInvitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = "Pending"};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation.Id, expectedInvitation.Id);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithEmptyNameThrowsException()
    {
        Mock<IInvitationRepository> repo = new Mock<IInvitationRepository>();
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "", RecipientEmail = "pepe@gmail.com"};
        Invitation expectedInvitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = "Pending"};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation.Id, expectedInvitation.Id);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithEmptyStatusThrowsException()
    {
        Mock<IInvitationRepository> repo = new Mock<IInvitationRepository>();
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = ""};
        Invitation expectedInvitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = "Pending"};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation.Id, expectedInvitation.Id);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithInvalidStatusThrowsException()
    {
        Mock<IInvitationRepository> repo = new Mock<IInvitationRepository>();
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = "invalidStatus"};
        Invitation expectedInvitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = "Pending"};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation.Id, expectedInvitation.Id);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithInvalidDeadLineThrowsException()
    {
        Mock<IInvitationRepository> repo = new Mock<IInvitationRepository>();
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(-2), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = "Pending"};
        Invitation expectedInvitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = "pepe", RecipientEmail = "pepe@gmail.com", Status = "Pending"};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation.Id, expectedInvitation.Id);
    }
}