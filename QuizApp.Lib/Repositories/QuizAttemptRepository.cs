using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class QuizAttemptRepository : Repository<QuizAttemptEntity>, IQuizAttemptRepository
{
    public QuizAttemptRepository(DataContext context) : base(context)
    {
    }
    // Additional specific methods for QuizAttemptEntity if needed
}
