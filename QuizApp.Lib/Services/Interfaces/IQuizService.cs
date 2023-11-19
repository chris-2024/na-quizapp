using QuizApp.Lib.Enums;
using QuizApp.Lib.Models;
using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Services;

public interface IQuizService
{
    List<CategoryEntity>? Categories { get; }
    Task<IEnumerable<Question>> GetRandomQuizQuestionsAsync(Difficulty difficulty, Language language);
    Task<bool> CreateQuizAttemptAsync(QuizAttemptEntity quizAttempt);
    Task<IEnumerable<QuizAttemptEntity>> GetAllQuizAttemptsAsync(User user);
    Task<bool> UpdateUserScoreAsync(UserScoreEntity userScore);
    Task<IEnumerable<UserScoreEntity>> GetAllUserScoresAsync();

}
