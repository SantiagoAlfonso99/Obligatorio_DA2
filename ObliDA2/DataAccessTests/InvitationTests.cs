using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class InvitationTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private InvitationRepository invitationRepository;
    private Invitation newInvitation;
    private const int InvitationId = 1;
    
    [TestInitialize]
    public void Initialize()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var contextOptions = new DbContextOptionsBuilder<DataAppContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new DataAppContext(contextOptions);
        _context.Database.EnsureCreated();
        
        invitationRepository = new InvitationRepository(_context);
        newInvitation = new Invitation() { Status = "Pending", Name = "raul", RecipientEmail = "raul@gmail.com", Role = "CompanyManager"};
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        _context.Invitations.Add(newInvitation);
        _context.SaveChanges();
        newInvitation.Id = InvitationId;
        
        List<Invitation> returnedInvitations = invitationRepository.GetAll();
        List<Invitation> expectedInvitations = new List<Invitation>()
            { newInvitation};
        
        CollectionAssert.AreEqual(expectedInvitations, returnedInvitations);
    }
    
    [TestMethod]
    public void GetOk()
    {
        _context.Invitations.Add(newInvitation);
        _context.SaveChanges();
        newInvitation.Id = InvitationId;
        
        Invitation returnedInvitation = invitationRepository.GetById(InvitationId);
        
        Assert.AreEqual(newInvitation, returnedInvitation);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        invitationRepository.Create(newInvitation);
        _context.SaveChanges();
        newInvitation.Id = InvitationId;
        
        Invitation returnedInvitation = invitationRepository.GetById(InvitationId);
        
        Assert.AreEqual(newInvitation, returnedInvitation);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Invitation otherInvitation = new Invitation() { Status = "Pending", Name = "pepe", RecipientEmail = "pepe@gmail.com", Role = "CompanyManager"};
        invitationRepository.Create(newInvitation);
        _context.SaveChanges();
        invitationRepository.Create(otherInvitation);
        invitationRepository.Delete(newInvitation);
        _context.SaveChanges();
        otherInvitation.Id = 2;
        
        
        List<Invitation> returnedInvitations = invitationRepository.GetAll();
        List<Invitation> expectedInvitations = new List<Invitation>()
            { otherInvitation};
        
        CollectionAssert.AreEqual(returnedInvitations, expectedInvitations);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        Invitation otherInvitation = new Invitation() { Status = "Pending", Name = "pepe", RecipientEmail = "pepe@gmail.com",Role = "CompanyManager"};
        invitationRepository.Create(newInvitation);
        _context.SaveChanges();
        invitationRepository.Create(otherInvitation);
        newInvitation.Id = InvitationId;
        newInvitation.Status = "Accepted";
        newInvitation.Name = "santiago";
        
        invitationRepository.Update(newInvitation);
        _context.SaveChanges();
        Invitation returnedInvitation = invitationRepository.GetById(InvitationId);
        
        Assert.AreEqual(newInvitation, returnedInvitation);
    }
}