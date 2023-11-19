using Microsoft.IdentityModel.Tokens;
using QuizApp.Enums;
using QuizApp.Lib.Models.Registrations;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class LoginMenu : IMenu
{
    private readonly MenuService _menuService;
    private readonly LoginService _loginService;

    public LoginMenu(MenuService menuService, LoginService loginService)
    {
        _menuService = menuService;
        _loginService = loginService;
    }

    public string Title { get; } = "MainMenu";

    public async Task ShowAsync()
    {
        bool validChoice = false;
        while (!_menuService.ExitApp && !validChoice)
        {
            _menuService.CurrentMenu = MenuType.LoginMenu;

            Console.Clear();

            _menuService.PrintMenuItems("Login", "Register", "Continue as Guest");

            var choice = _menuService.GetCharInput();

            switch (choice)
            {
                case '1':
                    validChoice = await _loginService.LoginUser(); // Hardcoded for simplicity in dev
                    break;
                case '2':
                    validChoice = await _loginService.RegisterUser(); // Hardcoded for simplicity in dev
                    break;
                case '3':
                    await _loginService.Guest();
                    validChoice = true;
                    break;
                case '0':
                    _menuService.ExitApp = true;
                    break;
                default:
                    break;
            }
        } 
    }
}
