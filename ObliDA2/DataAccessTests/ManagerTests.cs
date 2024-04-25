using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class ManagerTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private Manager newManager;
    private ManagerRepository managerRepository;
    private const int UserId = 1;
    private const bool ElementExists = true;
    private const bool ExistsFailed = false;
    
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

        managerRepository = new ManagerRepository(_context);
        
        newManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe@gmail.com"};
    }

    [TestMethod]
    public void GetAllOk()
    {
        _context.Managers.Add(newManager);
        _context.SaveChanges();
        
        List<Manager> returnedManager = managerRepository.GetAll().ToList();
        List<Manager> expectedManager = new List<Manager>()
            { new Manager() { Id = UserId, Name = "pepe", Password = "password", Email = "pepe@gmail.com" } };
        
        CollectionAssert.AreEqual(returnedManager, expectedManager);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        managerRepository.Add(newManager);
        _context.SaveChanges();
        
        List<Manager> returnedManager = managerRepository.GetAll().ToList();
        List<Manager> expectedManager = new List<Manager>()
            { new Manager() { Id = UserId, Name = "pepe", Password = "password", Email = "pepe@gmail.com" } };
        
        CollectionAssert.AreEqual(returnedManager, expectedManager);
    }
    
    [TestMethod]
    public void GetOk()
    {
        managerRepository.Add(newManager);
        _context.SaveChanges();
        
        Manager returnedManager = managerRepository.Get(UserId);
        newManager.Id = UserId;
        Assert.AreEqual(returnedManager, newManager);
    }
    
    [TestMethod]
    public void ExistsOk()
    {
        managerRepository.Add(newManager);
        _context.SaveChanges();
        
        bool success = managerRepository.Exists(UserId);
        Assert.AreEqual(ElementExists, success);
    }
    
    [TestMethod]
    public void ExistsReturnsFalse()
    {
        managerRepository.Add(newManager);
        _context.SaveChanges();
        
        bool success = managerRepository.Exists(2);
        Assert.AreEqual(ExistsFailed, success);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        managerRepository.Add(newManager);
        _context.SaveChanges();
        newManager.Id = UserId;
        newManager.Name = "luciano";
        newManager.Password = "newPassword";
        managerRepository.Update(newManager);
        _context.SaveChanges();
        
        List<Manager> returnedManager = managerRepository.GetAll().ToList();
        List<Manager> expectedManager = new List<Manager>()
            { new Manager() { Id = UserId, Name = "luciano", Password = "newPassword", Email = "pepe@gmail.com" } };
        
        CollectionAssert.AreEqual(returnedManager, expectedManager);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Manager otherManager = new Manager() { Name = "luciano", Password = "password", Email = "pepe2@gmail.com"};
        managerRepository.Add(newManager);
        _context.SaveChanges();
        managerRepository.Add(otherManager);
        managerRepository.Remove(newManager);
        _context.SaveChanges();
        
        List<Manager> returnedManager = managerRepository.GetAll().ToList();
        List<Manager> expectedManager = new List<Manager>()
            { new Manager() { Id = 2, Name = "luciano", Password = "password", Email = "pepe2@gmail.com" }};
        
        CollectionAssert.AreEqual(returnedManager, expectedManager);
    }
}