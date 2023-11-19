using QuizApp.Lib.Models.Entities;
using QuizApp.Lib.Models;
using System.Linq.Expressions;

namespace QuizApp.Lib.Services;

public interface IQuestionService
{
    Task<bool> CreateQuestionAsync(Question question);
    Task<bool> AddQuestionHistoryAsync(Question question);
    Task<IEnumerable<Question>> GetAllUserQuestionsAsync(int id);
    Task<IEnumerable<Question>> GetQuestionsAsync(Expression<Func<QuestionEntity, bool>> predicate);
}
