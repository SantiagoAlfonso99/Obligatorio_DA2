using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class ConstructionCompanyTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private ConstructionCompanyRepository constructionRepo;
    
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
        constructionRepo = new ConstructionCompanyRepository(_context);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        ConstructionCompany company = new ConstructionCompany() { Name = "construction1" };

        _context.ConstructionCompanies.Add(company);
        _context.SaveChanges();
        ConstructionCompany returnedCompany = constructionRepo.GetById(1);
        
        Assert.AreEqual(company,returnedCompany);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        ConstructionCompany company = new ConstructionCompany() { Name = "construction1" };

        constructionRepo.Create(company);
        ConstructionCompany returnedCompany = constructionRepo.GetById(1);
        
        Assert.AreEqual(company,returnedCompany);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        ConstructionCompany company = new ConstructionCompany() { Name = "construction1" };
        ConstructionCompany otherCompany = new ConstructionCompany() { Name = "construction2" };
        List<ConstructionCompany> expectedCompanies = new List<ConstructionCompany>() { company, otherCompany };
        
        constructionRepo.Create(company);
        constructionRepo.Create(otherCompany);
        List<ConstructionCompany> companies = constructionRepo.GetAll();
        
        CollectionAssert.AreEqual(expectedCompanies,companies);
    }
}