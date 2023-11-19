using Azure.Identity;
using QuizApp.Enums;
using QuizApp.Lib.Enums;
using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Models.Registrations;
using QuizApp.Lib.Services;
using QuizApp.Services;

namespace QuizApp.Menus;

internal class UserMenu : IMenu
{
    private readonly IUserService _userService;
    private readonly IQuestionService _questionService;
    private readonly IQuizService _quizService;
    private readonly MenuService _menuService;
    private readonly QuizMenu _quizMenu;
    private readonly LoginMenu _loginMenu;
    private bool exitUserMenu = false;

    public UserMenu(IUserService userService, IQuestionService questionService, IQuizService quizService, MenuService menuService, QuizMenu quizMenu, LoginMenu loginMenu)
    {
        _userService = userService;
        _questionService = questionService;
        _quizService = quizService;
        _menuService = menuService;
        _quizMenu = quizMenu;
        _loginMenu = loginMenu;
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
        return _userService.IsRegistered ? ["Generate New Question", "My Questions", "My Answered Questions", "Quiz Attempts", "Edit Account", "Delete Account"] : ["Register"];
    }

    private async Task MenuChoiceAsync(char choice)
    {
        if (_userService.IsRegistered)
            switch (choice)
            {
                case '1':
                    await _questionService.CreateQuestionAsync(new() { 
                        Category = new() { CategoryID = 2 }, 
                        Difficulty = Difficulty.Hard, 
                        Language = Language.English, 
                        QuestionText = "FakeGeneratedQuestion_" + Guid.NewGuid().ToString(),
                        UserID = _userService.CurrentUser!.Id,
                        Answers = new() { 
                            new() { AnswerText = "Answer 1" + Guid.NewGuid().ToString(), IsCorrect = false },
                            new() { AnswerText = "Answer 2" + Guid.NewGuid().ToString(), IsCorrect = false },
                            new() { AnswerText = "Answer 3" + Guid.NewGuid().ToString(), IsCorrect = true },
                        }
                    });
                    break;
                case '2':
                    await ShowQuestions();
                    break;
                case '3':
                    await ShowQuestionHistory();
                    break;
                case '4':
                    await ShowQuizAttempts();
                    break;
                case '5':
                    exitUserMenu = await UpdateUser();
                    Console.ReadKey();
                    break;
                case '6':
                    _menuService.ExitApp = await _userService.RemoveUserAsync();
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
                    await _loginMenu.ShowAsync();
                    break;
                case '0':
                    exitUserMenu = true;
                    break;
                default:
                    break;
            }
    }

    private async Task ShowQuizAttempts()
    {
        Console.Clear();
        var quizAttempts = await _quizService.GetAllQuizAttemptsAsync(_userService.CurrentUser!);

        foreach ( var quizAttempt in quizAttempts )
        {
            Console.WriteLine("---");
            Console.WriteLine("Questions: " + quizAttempt.TotalQuestionsAttempted);
            Console.WriteLine("Correct: " + quizAttempt.CorrectAnswersCount);
            Console.WriteLine("Incorrect: " + quizAttempt.IncorrectAnswersCount);
        }
        Console.ReadKey();
    }

    private async Task ShowQuestionHistory()
    {
        Console.Clear();
        var questionHistory = await _userService.GetAllUserQuestionHistoryAsync();

        foreach (var question in questionHistory)
        {
            Console.WriteLine(question.Question.QuestionText + " - " + question.AnsweredDate);
        }
        Console.ReadKey();
    }

    private async Task<bool> UpdateUser()
    {
        Console.Clear();
        var user = new UserRegistration();

        Console.Write("Username: ");
        user.Username = Console.ReadLine() ?? string.Empty;
        Console.Write("Password: ");
        user.Password = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
        {
            Console.Write("\nInvalid input");
            return false;
        }

        var result = await _userService.UpdateUserAsync(user);

        Console.WriteLine("\n" + result.Message);
        return result.Result;
    }

    private async Task ShowQuestions()
    {
        Console.Clear();
        var questions = await _questionService.GetAllUserQuestionsAsync(_userService.CurrentUser!.Id);
        questions.ToList().ForEach(q => { Console.WriteLine(q.QuestionText); });
        Console.ReadKey();
    }
}
