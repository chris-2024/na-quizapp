using QuizApp.Menus;
using QuizApp.Services;

namespace QuizApp;

internal class Application
{
    private readonly IMenuService _menuService;
    private readonly LoginMenu _loginMenu;
    private readonly MainMenu _mainMenu;

    public Application(IMenuService menuService, LoginMenu loginMenu, MainMenu mainMenu)
    {
        _menuService = menuService;
        _loginMenu = loginMenu;
        _mainMenu = mainMenu;
    }

    public async Task Run()
    {
        do
        {
            await _loginMenu.ShowAsync();
            await _mainMenu.ShowAsync();

        } while (!_menuService.ExitApp);

        Console.WriteLine("Exiting...");
    }
}
