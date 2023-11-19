using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class QuestionRepository : Repository<QuestionEntity>, IQuestionRepository
{
    public QuestionRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for QuestionEntity if needed
}
