using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class AdminTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private AdminRepository adminRepository;
    private Admin newAdmin;
    private const int UserId = 1;
    
    [TestInitialize]
    public void Setup()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var contextOptions = new DbContextOptionsBuilder<DataAppContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new DataAppContext(contextOptions);
        _context.Database.EnsureCreated();

        adminRepository = new AdminRepository(_context);
        newAdmin = new Admin() { Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com"};
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        Admin otherAdmin = new Admin() { Name = "pepe2", LastName = "suarez", Password = "password", Email = "pepe2@gmail.com"};
        adminRepository.Add(newAdmin);
        adminRepository.Add(otherAdmin);
        _context.SaveChanges();
        newAdmin.Id = UserId;
        otherAdmin.Id = 2;
        
        List<Admin> returnedAdmins = adminRepository.GetAll();
        List<Admin> expectedAdmins = new List<Admin>()
            { newAdmin, otherAdmin };

        bool correctReturn = (returnedAdmins[0].AreEqual(expectedAdmins[0])) &&
                             (returnedAdmins[1].AreEqual(expectedAdmins[1]));
        Assert.IsTrue(correctReturn);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        adminRepository.Add(newAdmin);
        _context.SaveChanges();
        
        List<Admin> returnedAdmins = adminRepository.GetAll();
        List<Admin> expectedAdmins = new List<Admin>()
            { new Admin() { Id = UserId, Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com" } };
        
        Assert.IsTrue(expectedAdmins[0].AreEqual(returnedAdmins[0]));
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        adminRepository.Add(newAdmin);
        Admin otherAdmin = new Admin() { Name = "pepe2", LastName = "suarez", Password = "password", Email = "pepe2@gmail.com"};
        adminRepository.Add(otherAdmin);
        _context.SaveChanges();
        
        adminRepository.Remove(otherAdmin);
        _context.SaveChanges();
        List<Admin> returnedAdmins = adminRepository.GetAll();
        
        Assert.AreEqual(UserId, returnedAdmins.Count);
    }
    
    [TestMethod]
    public void GetOk()
    {
        adminRepository.Add(newAdmin);
        _context.SaveChanges();
        
        Admin returnedAdmin = adminRepository.Get(UserId);
        
        Assert.IsTrue(returnedAdmin.AreEqual(newAdmin));
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        adminRepository.Add(newAdmin);
        _context.SaveChanges();
        newAdmin.LastName = "perez";
        newAdmin.Id = 1;
        
        adminRepository.Update(newAdmin);
        _context.SaveChanges();
        Admin returnedAdmin = adminRepository.Get(UserId);
        
        Assert.IsTrue(returnedAdmin.AreEqual(newAdmin));
    }
}