using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Repositories;
using QuizApp.Lib.Services;
using QuizApp.Menus;
using QuizApp.Services;

namespace QuizApp;

internal class Program
{
    private static readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Code\QuizApp\QuizApp.Lib\Contexts\quizapp_localdb.mdf;Integrated Security=True;Connect Timeout=30";

    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        // DbContext
        services.AddDbContext<DataContext>(options => options.UseSqlServer(_connectionString));
        // App
        services.AddScoped<Application>();
        // Menus
        services.AddScoped<LoginMenu>();
        services.AddScoped<MainMenu>();
        services.AddScoped<UserMenu>();
        services.AddScoped<QuizMenu>();
        // Services
        services.AddScoped<MenuService>();
        services.AddScoped<LoginService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IQuizService, QuizService>();
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserQuestionHistoryRepository, UserQuestionHistoryRepository>();
        services.AddScoped<IUserScoreRepository, UserScoreRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // Build
        var serviceProvider = services.BuildServiceProvider();

        // Get application
        var app = serviceProvider.GetRequiredService<Application>();

        // Run application
        await app.Run();
    }
}