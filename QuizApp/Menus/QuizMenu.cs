using Azure.Identity;
using QuizApp.Enums;
using QuizApp.Lib.Enums;
using QuizApp.Lib.Migrations;
using QuizApp.Lib.Models;
using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Services;
using QuizApp.Services;
using System.Diagnostics;

namespace QuizApp.Menus;

internal class QuizMenu : IMenu
{
    private readonly MenuService _menuService;
    private readonly IQuizService _quizService;
    private readonly IUserService _userService;
    private readonly IQuestionService _questionService;
    private bool leaveQuizMenu = false;

    public QuizMenu(MenuService menuService, IQuizService quizService, IUserService userService, IQuestionService questionService)
    {
        _menuService = menuService;
        _quizService = quizService;
        _userService = userService;
        _questionService = questionService;
    }

    public string Title { get; } = "Quizzes";

    public async Task ShowAsync()
    {
        while (!leaveQuizMenu) 
        { 
            _menuService.CurrentMenu = MenuType.QuizMenu;

            Console.Clear();

            _menuService.PrintMenuItems(["Eng Quiz - Normal", "Eng Quiz - Hard", "Swe Quiz - Normal", "Swe Quiz - Hard"]);

            var choice = _menuService.GetCharInput();

            leaveQuizMenu = await MenuSelection(choice);
        }
    }

    private async Task<bool> MenuSelection(char choice)
    {
        switch (choice)
        {
            case '1':
                await StartNewQuiz(Language.English, Difficulty.Normal);
                return false;
            case '2':
                await StartNewQuiz(Language.English, Difficulty.Hard);
                return false;
            case '3':
                await StartNewQuiz(Language.Swedish, Difficulty.Normal);
                return false;
            case '4':
                await StartNewQuiz(Language.Swedish, Difficulty.Hard);
                return false;
            case '0':
                return true;
            default:
                break;
        }

        return true;
    }

    private async Task StartNewQuiz(Language language, Difficulty difficulty)
    {
        try
        {
            var questions = await _quizService.GetRandomQuizQuestionsAsync(difficulty, language);

            if (questions == null || !questions.Any())
            {
                Console.Write("No questions available.");
                return;
            }

            var quizAttempt = await RunQuiz(questions.ToList());

            // Only save the quizattempt if currentuser is registered
            if (quizAttempt != null && quizAttempt.UserID != 0)
                await _quizService.CreateQuizAttemptAsync(quizAttempt);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Console.Write("\n Quiz encountered an error...");
        }

        Console.ReadKey();
    }

    private async Task<QuizAttemptEntity> RunQuiz(List<Question> questions)
    {
        bool quizRunning = true;
        bool hasCorrectAnswer = false;

        var quizAttempt = new QuizAttemptEntity();
        quizAttempt.AttemptDate = DateTime.Now.Date;

        if (_userService.CurrentUser!.UserRole is UserRole.Registered)
            quizAttempt.UserID = _userService.CurrentUser!.Id;

        while (quizRunning)
        {
            foreach (var question in questions)
            {
                Console.Clear();

                // If there are no answers, skip question
                if (question.Answers == null && !question.Answers!.Any()) continue;

                Console.WriteLine(question);
                Console.WriteLine();
                for (int i = 0; i < question.Answers!.Count; i++)
                {
                    var answer = question.Answers![i];
                    Console.WriteLine($"{i + 1}. {answer.AnswerText}");
                    if (answer.IsCorrect) 
                        hasCorrectAnswer = true;
                }

                Console.WriteLine("0. End Quiz");

                // If there are no possible correct answers, skip question
                if (!hasCorrectAnswer) continue;

                int userChoice = -1;
                bool validChoice = false;

                while (!validChoice)
                {
                    var input = _menuService.GetCharInput();
                    if (int.TryParse(input.ToString(), out userChoice))
                    {
                        if (userChoice >= 0 && userChoice <= question.Answers!.Count)
                        {
                            validChoice = true;
                        }
                    }
                }

                if (userChoice == 0)
                {
                    // End the quiz
                    quizRunning = false;
                    break;
                }

                // Check if the selected answer is correct
                var selectedAnswer = question.Answers[userChoice - 1];
                if (selectedAnswer.IsCorrect)
                {
                    Console.WriteLine("Correct!");
                    quizAttempt.CorrectAnswersCount++;
                }
                else
                {
                    Console.WriteLine("Incorrect.");
                    quizAttempt.IncorrectAnswersCount++;
                }

                await _questionService.AddQuestionHistoryAsync(question);
                quizAttempt.TotalQuestionsAttempted++;
            }

            quizRunning = false;
        }

        return quizAttempt;
    }
}