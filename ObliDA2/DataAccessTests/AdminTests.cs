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
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        Admin newAdmin = new Admin() { Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com"};
        Admin otherAdmin = new Admin() { Name = "pepe2", LastName = "suarez", Password = "password", Email = "pepe2@gmail.com"};
        adminRepository.Add(newAdmin);
        adminRepository.Add(otherAdmin);
        _context.SaveChanges();
        newAdmin.Id = 1;
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
        Admin newAdmin = new Admin() { Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com"};
        adminRepository.Add(newAdmin);
        _context.SaveChanges();
        
        List<Admin> returnedAdmins = adminRepository.GetAll();
        List<Admin> expectedAdmins = new List<Admin>()
            { new Admin() { Id = 1, Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com" } };
        
        Assert.IsTrue(expectedAdmins[0].AreEqual(returnedAdmins[0]));
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Admin newAdmin = new Admin() { Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com"};
        adminRepository.Add(newAdmin);
        Admin otherAdmin = new Admin() { Name = "pepe2", LastName = "suarez", Password = "password", Email = "pepe2@gmail.com"};
        adminRepository.Add(otherAdmin);
        _context.SaveChanges();
        
        adminRepository.Remove(otherAdmin);
        _context.SaveChanges();
        List<Admin> returnedAdmins = adminRepository.GetAll();
        
        Assert.AreEqual(1, returnedAdmins.Count);
    }
    
    [TestMethod]
    public void GetOk()
    {
        Admin newAdmin = new Admin() { Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com"};
        adminRepository.Add(newAdmin);
        _context.SaveChanges();
        
        Admin returnedAdmin = adminRepository.Get(1);
        
        Assert.IsTrue(returnedAdmin.AreEqual(newAdmin));
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        Admin newAdmin = new Admin() { Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com"};
        adminRepository.Add(newAdmin);
        _context.SaveChanges();
        newAdmin.LastName = "perez";
        newAdmin.Id = 1;
        
        adminRepository.Update(newAdmin);
        _context.SaveChanges();
        Admin returnedAdmin = adminRepository.Get(1);
        
        Assert.IsTrue(returnedAdmin.AreEqual(newAdmin));
    }
}