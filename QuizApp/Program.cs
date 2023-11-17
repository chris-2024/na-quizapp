using Microsoft.Extensions.DependencyInjection;

namespace QuizApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddScoped<Application>()
            // Menus

            // Services

            // Repositories

            // Build
            .BuildServiceProvider();

            // Get application
            var app = serviceProvider.GetRequiredService<Application>();

            // Run application
            await app.Run();
        }
    }
}
