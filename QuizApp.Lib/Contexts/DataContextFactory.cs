using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace QuizApp.Lib.Contexts;

internal class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Code\QuizApp\QuizApp.Lib\Contexts\quizapp_localdb.mdf;Integrated Security=True;Connect Timeout=30";

    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(_connectionString);
        return new DataContext(optionsBuilder.Options);
    }
}
