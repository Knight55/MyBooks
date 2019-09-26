using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyBooks.Dal.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var basePath = AppContext.BaseDirectory;
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return Create(basePath, environmentName);
        }

        private ApplicationDbContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true);

            var config = builder.Build();
            var connectionString = config.GetConnectionString("SqlServerConnection");

            if (string.IsNullOrWhiteSpace(connectionString) == true)
            {
                throw new InvalidOperationException(
                    "Could'nt find a connection string named 'SqlServerConnection'");
            }
            else
            {
                return Create(connectionString);
            }
        }

        private ApplicationDbContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}