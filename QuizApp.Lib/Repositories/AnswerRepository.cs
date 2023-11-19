using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class AnswerRepository : Repository<AnswerEntity>, IAnswerRepository
{
    public AnswerRepository(DataContext context) : base(context)
    {
    }
    // Additional specific methods for AnswerEntity if needed
}
