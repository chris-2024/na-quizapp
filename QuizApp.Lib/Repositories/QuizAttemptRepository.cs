using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class QuizAttemptRepository : Repository<QuizAttemptEntity>, IQuizAttemptRepository
{
    public QuizAttemptRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for QuizAttemptEntity if needed
}
