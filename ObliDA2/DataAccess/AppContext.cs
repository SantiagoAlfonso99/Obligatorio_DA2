using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Models;

namespace DataAccess;

public class AppContext : DbContext
{
    public AppContext() { }
    public AppContext(DbContextOptions options) : base(options) { }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string directory = Directory.GetCurrentDirectory();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString(@"obliDA2DB");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}