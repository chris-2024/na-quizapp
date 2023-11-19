using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class AnswerRepository : Repository<AnswerEntity>, IAnswerRepository
{
    public AnswerRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for AnswerEntity if needed
}
