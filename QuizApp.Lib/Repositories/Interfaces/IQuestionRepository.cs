using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public interface IQuestionRepository : IRepository<QuestionEntity>
{
    // Additional specific methods for QuestionEntity if needed
}
