using QuizApp.Enums;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class MainMenu : IMenu
{
    private readonly IMenuService _menuService;
    private readonly UserMenu _userMenu;
    private readonly QuizMenu _quizMenu;

    public MainMenu(IMenuService menuService, UserMenu userMenu, QuizMenu quizMenu)
    {
        _menuService = menuService;
        _userMenu = userMenu;
        _quizMenu = quizMenu;
    }

    public string Title { get; } = "MainMenu";

    public async Task ShowAsync()
    {
        while(!_menuService.ExitApp)
        {
            _menuService.CurrentMenu = MenuType.MainMenu;

            Console.Clear();

            string loginLogoutOption = false ? "Logout" : "Login";
            _menuService.PrintMenuItems(_quizMenu.Title, _userMenu.Title, "Scoreboard", loginLogoutOption);

            if (await MenuSelection(_menuService.GetCharInput()))
                break;
        }
    }

    private async Task<bool> MenuSelection(char choice)
    {
        switch (choice)
        {
            case '1':
                await _quizMenu.ShowAsync();
                break;
            case '2':
                await _userMenu.ShowAsync();
                break;
            case '3':
                await ShowScores();
                break;
            case '4':
                return true;
            case '0':
                _menuService.ExitApp = true;
                break;
            default:
                break;
        }

        return false;
    }

    private async Task ShowScores()
    {
        Console.Clear();
        await Console.Out.WriteLineAsync("1. ScoreOne");
        await Console.Out.WriteLineAsync("2. ScoreTwo");
        Console.ReadKey();
    }
}