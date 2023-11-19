using QuizApp.Lib.Enums;
using QuizApp.Lib.Models;
using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Repositories;
using System.Diagnostics;

namespace QuizApp.Lib.Services;

public class QuizService : IQuizService
{
    private readonly IQuestionService questionService;
    private readonly IUserScoreRepository userScoreRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IQuizAttemptRepository quizAttemptRepository;

    public QuizService(IQuestionService questionService, IUserScoreRepository userScoreRepository, ICategoryRepository categoryRepository, IQuizAttemptRepository quizAttemptRepository)
    {
        this.questionService = questionService;
        this.userScoreRepository = userScoreRepository;
        this.categoryRepository = categoryRepository;
        this.quizAttemptRepository = quizAttemptRepository;
    }

    public List<CategoryEntity>? Categories { get; private set; }

    public async Task<IEnumerable<Question>> GetRandomQuizQuestionsAsync(Difficulty difficulty, Language language)
    {
        try
        {
            if (Categories == null)
                await GetAllCategories();

            Random rnd = new();
            int randomCategoryId = rnd.Next(1, Categories!.Count);

            var questions = await questionService.GetQuestionsAsync(q => q.CategoryID == randomCategoryId && q.DifficultyID == (int)difficulty && q.LanguageID == (int)language);
            if (questions != null && questions.Any()) 
                return questions;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> CreateQuizAttemptAsync(QuizAttemptEntity quizAttempt)
    {
        try
        {
            var result = await quizAttemptRepository.CreateAsync(quizAttempt);

            if (result != null)
                await UpdateUserScoreAsync(quizAttempt);

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // Get user quiz attempts 
    public async Task<IEnumerable<QuizAttemptEntity>> GetAllQuizAttemptsAsync(User user)
    {
        try
        {
            if (user.UserRole != UserRole.Registered)
                return null!;

            var result = await quizAttemptRepository.ReadRangeAsync(q => q.UserID == user.Id);
            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateUserScoreAsync(UserScoreEntity userScore)
    {
        bool result = false;

        try
        {
            // Check if the user already has a score
            var existingUserScore = await userScoreRepository.ReadAsync(us => us.UserID == userScore.UserID);

            if (existingUserScore != null)
            {
                existingUserScore.IncorrectAnswers += userScore.IncorrectAnswers;
                existingUserScore.CorrectAnswers += userScore.CorrectAnswers;
                existingUserScore.LastScoredDate = DateTime.Now.Date;
                result = await userScoreRepository.UpdateAsync(existingUserScore) != null;
            }
            else
                result = await userScoreRepository.CreateAsync(userScore) != null;

            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return result;
    }

    public async Task<IEnumerable<UserScoreEntity>> GetAllUserScoresAsync()
    {
        try
        {
            var scores = await userScoreRepository.ReadAllAsync() ?? null!;
            return scores;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    private async Task GetAllCategories()
    {
        try
        {
            Categories = new(await categoryRepository.ReadAllAsync());
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }
}
