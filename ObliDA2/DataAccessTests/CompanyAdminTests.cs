using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class CompanyAdminTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private CompanyAdminRepository _repository;
    
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
        _repository = new CompanyAdminRepository(_context);
    }

    [TestMethod]
    public void GetAllOk()
    {
        CompanyAdmin admin = new CompanyAdmin() { Name = "pepe", Email = "pep@gmail.com", Password = "password" };
        CompanyAdmin otherAdmin = new CompanyAdmin() { Name = "pepe2", Email = "pep2@gmail.com", Password = "password2" };
        _context.CompanyAdmins.Add(admin);
        _context.SaveChanges();
        _context.CompanyAdmins.Add(otherAdmin);
        _context.SaveChanges();
        List<CompanyAdmin> expectedAdmins = new List<CompanyAdmin>() { admin, otherAdmin };
        
        List<CompanyAdmin> returnedAdmins = _repository.GetAll();
        
        CollectionAssert.AreEqual(expectedAdmins,returnedAdmins);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        CompanyAdmin admin = new CompanyAdmin() { Name = "pepe", Email = "pep@gmail.com", Password = "password" };
        List<CompanyAdmin> expectedAdmins = new List<CompanyAdmin>() { admin };
        
        _repository.Create(admin);
        List<CompanyAdmin> returnedAdmins = _repository.GetAll();
        
        CollectionAssert.AreEqual(expectedAdmins,returnedAdmins);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        CompanyAdmin admin = new CompanyAdmin() { Name = "pepe", Email = "pep@gmail.com", Password = "password" };
        _repository.Create(admin);
        admin.Company = new ConstructionCompany() { Name = "name"};
        _repository.Update(admin);
        CompanyAdmin returnedAdmin = _repository.GetAll().FirstOrDefault(companyAdmin => companyAdmin.Id == 1);
        
        Assert.AreEqual(admin.Company,returnedAdmin.Company);
    }
}