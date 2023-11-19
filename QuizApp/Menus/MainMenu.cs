using QuizApp.Enums;
using QuizApp.Lib.Enums;
using QuizApp.Lib.Services;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class MainMenu : IMenu
{
    private readonly IMenuService _menuService;
    private readonly IUserService _userService;
    private readonly UserMenu _userMenu;
    private readonly QuizMenu _quizMenu;

    public MainMenu(IMenuService menuService, IUserService userService, UserMenu userMenu, QuizMenu quizMenu)
    {
        _menuService = menuService;
        _userService = userService;
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

            if (_userService.CurrentUser == null)
            {
                Console.WriteLine("Failed to establish User");
                _menuService.ExitApp = true;
            }

            string loginLogoutOption = _userService.CurrentUser!.UserRole is UserRole.Registered ? "Logout" : "Login";
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

        var scores = await _userService.GetAllUserScores();

        if (scores == null || scores.Count() <= 0)
            Console.WriteLine("No Scores");
        else
        {
            var sortedScores = scores.OrderByDescending(s => s.CorrectAnswers).ToList();

            for (int i = 0; i < sortedScores!.Count; i++)
            {
                Console.WriteLine($"#{i + 1} {sortedScores[i].User.Username} - {sortedScores[i].CorrectAnswers}");
            }

        }

        Console.ReadKey();
    }
}