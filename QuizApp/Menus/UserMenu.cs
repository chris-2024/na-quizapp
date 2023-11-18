using QuizApp.Enums;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class UserMenu : IMenu
{
    private readonly IMenuService _menuService;
    private readonly QuizMenu _quizMenu;
    private readonly LoginMenu loginMenu;
    private bool isRegistered = false;
    private bool exitUserMenu = false;

    public UserMenu(IMenuService menuService, QuizMenu quizMenu, LoginMenu loginMenu)
    {
        _menuService = menuService;
        _quizMenu = quizMenu;
        this.loginMenu = loginMenu;
    }

    public string Title { get; } = "Settings";

    public async Task ShowAsync()
    {
        exitUserMenu = false;
        do
        {
            _menuService.CurrentMenu = MenuType.UserMenu;

            Console.Clear();
            Console.WriteLine("Username");
            _menuService.PrintMenuItems(UserMenuItems());

            char choice = _menuService.GetCharInput();
            await MenuChoiceAsync(choice);
        }while (!exitUserMenu);
    }

    private string[] UserMenuItems()
    {
        return isRegistered ? ["Quiz History", "My Questions", "Edit Account", "Delete Account"] : ["Register"];
    }

    private async Task MenuChoiceAsync(char choice)
    {
        if (isRegistered)
            switch (choice)
            {
                case '1':
                    Console.WriteLine("\nQuiz History");
                    break;
                case '2':
                    Console.WriteLine("\nMy Questions");
                    break;
                case '3':
                    Console.WriteLine("\nEdit Account");
                    break;
                case '4':
                    Console.WriteLine("\nDelete account");
                    break;
                case '0':
                    exitUserMenu = true;
                    break;
                default:
                    break;
            }
        else
            switch (choice)
            {
                case '1':
                    exitUserMenu = true;
                    await loginMenu.ShowAsync();
                    break;
                case '0':
                    exitUserMenu = true;
                    break;
                default:
                    break;
            }
    }
}
