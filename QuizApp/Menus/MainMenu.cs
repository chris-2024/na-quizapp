using QuizApp.Enums;
using QuizApp.Lib.Enums;
using QuizApp.Lib.Services;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class MainMenu : IMenu
{
    private readonly IUserService _userService;
    private readonly IQuizService _quizService;
    private readonly MenuService _menuService;
    private readonly UserMenu _userMenu;
    private readonly QuizMenu _quizMenu;

    public MainMenu(MenuService menuService, IUserService userService, IQuizService quizService, UserMenu userMenu, QuizMenu quizMenu)
    {
        _menuService = menuService;
        _userService = userService;
        _quizService = quizService;
        _userMenu = userMenu;
        _quizMenu = quizMenu;
    }

    public string Title { get; } = "MainMenu";

    public async Task ShowAsync()
    {
        while(!_menuService.ExitApp && _userService.CurrentUser != null)
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

            await MenuSelection(_menuService.GetCharInput());
        }
    }

    private async Task MenuSelection(char choice)
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
                _userService.CurrentUser = null;
                break;
            case '0':
                _menuService.ExitApp = true;
                break;
            default:
                break;
        }
    }

    private async Task ShowScores()
    {
        Console.Clear();

        var scores = await _quizService.GetAllUserScoresAsync();

        if (scores == null || scores.Count() <= 0)
            Console.WriteLine("No Scores");
        else
        {
            var sortedScores = scores.OrderByDescending(s => s.CorrectAnswers).ToList();

            for (int i = 0; i < sortedScores!.Count; i++)
            {
                Console.WriteLine($"#{i + 1} |{sortedScores[i].User?.Username ?? string.Empty}| Correct: {sortedScores[i].CorrectAnswers} | Incorrect: {sortedScores[i].IncorrectAnswers}");
            }

        }

        Console.ReadKey();
    }
}