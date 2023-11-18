namespace QuizApp.Menus;

internal interface IMenu
{
    string Title { get; }
    Task ShowAsync();
}
