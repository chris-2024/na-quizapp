using QuizApp.Enums;

namespace QuizApp.Services;

internal interface IMenuService
{
    bool ExitApp { get; set; }

    MenuType CurrentMenu { get; set; }

    char GetCharInput();

    string GetStringInput();

    void PrintMenuItems(params string[] menuItems);
}
