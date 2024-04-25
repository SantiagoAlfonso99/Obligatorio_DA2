using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class ManagerTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private ManagerRepository managerRepository;
    
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
    }

    [TestMethod]
    public void GetAllOk()
    {
        Manager newManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe@gmail.com"};
        _context.Managers.Add(newManager);
        _context.SaveChanges();
        
        List<Manager> returnedManager = managerRepository.GetAll().ToList();
        List<Manager> expectedManager = new List<Manager>()
            { new Manager() { Id = 1, Name = "pepe", Password = "password", Email = "pepe@gmail.com" } };
        
        CollectionAssert.AreEqual(returnedManager, expectedManager);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Manager newManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe@gmail.com"};
        managerRepository.Add(newManager);
        _context.SaveChanges();
        
        List<Manager> returnedManager = managerRepository.GetAll().ToList();
        List<Manager> expectedManager = new List<Manager>()
            { new Manager() { Id = 1, Name = "pepe", Password = "password", Email = "pepe@gmail.com" } };
        
        CollectionAssert.AreEqual(returnedManager, expectedManager);
    }
    
    [TestMethod]
    public void GetOk()
    {
        Manager newManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe@gmail.com"};
        managerRepository.Add(newManager);
        _context.SaveChanges();
        
        Manager returnedManager = managerRepository.Get(1);
        newManager.Id = 1;
        Assert.AreEqual(returnedManager, newManager);
    }
    
    [TestMethod]
    public void ExistsOk()
    {
        Manager newManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe@gmail.com"};
        managerRepository.Add(newManager);
        _context.SaveChanges();
        
        bool success = managerRepository.Exists(1);
        Assert.AreEqual(true, success);
    }
    
    [TestMethod]
    public void ExistsReturnsFalse()
    {
        Manager newManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe@gmail.com"};
        managerRepository.Add(newManager);
        _context.SaveChanges();
        
        bool success = managerRepository.Exists(2);
        Assert.AreEqual(false, success);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        Manager newManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe@gmail.com"};
        managerRepository.Add(newManager);
        _context.SaveChanges();
        newManager.Id = 1;
        newManager.Name = "luciano";
        newManager.Password = "newPassword";
        managerRepository.Update(newManager);
        _context.SaveChanges();
        
        List<Manager> returnedManager = managerRepository.GetAll().ToList();
        List<Manager> expectedManager = new List<Manager>()
            { new Manager() { Id = 1, Name = "luciano", Password = "newPassword", Email = "pepe@gmail.com" } };
        
        CollectionAssert.AreEqual(returnedManager, expectedManager);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Manager newManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe@gmail.com"};
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