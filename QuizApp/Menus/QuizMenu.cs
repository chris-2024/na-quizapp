using QuizApp.Enums;
using QuizApp.Lib.Services;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class QuizMenu : IMenu
{
    private readonly IMenuService _menuService;
    private readonly IQuizService _quizService;

    public QuizMenu(IMenuService menuService, IQuizService quizService)
    {
        _menuService = menuService;
        _quizService = quizService;
    }

    public string Title { get; } = "Quizzes";

    public async Task ShowAsync()
    {
        while (true) 
        { 
            _menuService.CurrentMenu = MenuType.QuizMenu;
            await Console.Out.WriteLineAsync();
            Console.Clear();

            _menuService.PrintMenuItems(["Eng Quiz - Hard", "Eng Quiz - Hard", "Swe Quiz - Normal", "Swe Quiz - Hard"]);

            var choice = _menuService.GetCharInput();

            if (choice == '0') break;
        }
    }
}
