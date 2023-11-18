using QuizApp.Enums;
using System.Diagnostics;

namespace QuizApp.Services;

internal class MenuService : IMenuService
{
    public MenuService()
    {
    }

    public bool ExitApp { get; set; } = false;

    public MenuType CurrentMenu { get; set; }

    public char GetCharInput()
    {
        try
        {
            Console.Write("\nChoose: ");

            return Console.ReadKey().KeyChar;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return ' ';
    }

    public string GetStringInput()
    {
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public void PrintMenuItems(params string[] menuItems)
    {
        try
        {
            if (menuItems == null || menuItems.Length == 0)
                throw new InvalidOperationException("No menu items provided.");

            for (int i = 0; i < menuItems.Length; i++)
                Console.WriteLine($"{i + 1}. {menuItems[i]}");

            string menuItem = CurrentMenu is MenuType.MainMenu || CurrentMenu is MenuType.LoginMenu ? "Exit" : "Back";
            Console.WriteLine("0. " + menuItem);
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex.Message);
            Console.WriteLine("Failed to print menu.\nExiting...");
            Environment.Exit(0);
        }
    }
}
