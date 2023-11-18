﻿using QuizApp.Enums;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class LoginMenu : IMenu
{
    private readonly IMenuService _menuService;
    private readonly LoginService _loginService;

    public LoginMenu(IMenuService menuService, LoginService loginService) // Move login logic to UserService in repo
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
                    await _loginService.LoginUser("Kalle", "123");
                    validChoice = true;
                    break;
                case '2':
                    await _loginService.RegisterUser("Findus", "123");
                    validChoice = true;
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