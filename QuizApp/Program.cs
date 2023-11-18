using Microsoft.Extensions.DependencyInjection;
using QuizApp.Menus;
using QuizApp.Services;

namespace QuizApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
        .AddScoped<Application>()
        // Menus
        .AddScoped<LoginMenu>()
        .AddScoped<MainMenu>()
        .AddScoped<UserMenu>()
        .AddScoped<QuizMenu>()
        // Services
        .AddScoped<IMenuService, MenuService>()
        .AddScoped<LoginService>()
        .AddScoped<QuizService>()
        // Repositories

        // Build
        .BuildServiceProvider();

        // Get application
        var app = serviceProvider.GetRequiredService<Application>();

        // Run application
        await app.Run();
    }
}