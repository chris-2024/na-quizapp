using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace QuizApp.Lib.Contexts;

internal class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    private static readonly string _connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;

    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(_connectionString);
        return new DataContext(optionsBuilder.Options);
    }
}
