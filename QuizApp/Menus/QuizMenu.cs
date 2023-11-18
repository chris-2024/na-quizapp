using QuizApp.Enums;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class QuizMenu : IMenu
{
    private readonly IMenuService _menuService;
    private readonly QuizService _quizService;

    public QuizMenu(IMenuService menuService, QuizService quizService)
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

            Console.Clear();

            _menuService.PrintMenuItems(_quizService.QuizOptions());

            var choice = _menuService.GetCharInput();

            if (choice == '0') break;
        }
    }
}
