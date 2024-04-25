using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class CategoryTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private CategoryRepository categoryRepository;
    private Category newCategory;
    
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
        
        categoryRepository = new CategoryRepository(_context);
        newCategory = new Category() { Name = "Vecino Molesto" };
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        List<Category> expectedCategories = new List<Category>()
            { new Category() { Id = 1, Name = "Vecino Molesto"} };
        
        List<Category> returnedCategories = categoryRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedCategories, returnedCategories);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        newCategory.Id = 1;
        
        Category returnedCategory = categoryRepository.GetById(1);
        
        Assert.AreEqual(newCategory, returnedCategory);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        categoryRepository.Create(newCategory);
        _context.SaveChanges();
        newCategory.Id = 1;
        List<Category> expectedCategories = new List<Category>()
            { newCategory };
        
        
        List<Category> returnedCategories = categoryRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedCategories, returnedCategories);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Category otherCategory = new Category() { Name = "Electricista" };
        
        categoryRepository.Create(newCategory);
        _context.SaveChanges();
        newCategory.Id = 1;
        categoryRepository.Create(otherCategory);
        categoryRepository.Delete(newCategory);
        _context.SaveChanges();
        otherCategory.Id = 2;
        List<Category> expectedCategories = new List<Category>()
            { otherCategory };
        
        
        List<Category> returnedCategories = categoryRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedCategories, returnedCategories);
    }
}