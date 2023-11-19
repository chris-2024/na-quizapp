using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class UserQuestionHistoryRepository : Repository<UserQuestionHistoryEntity>, IUserQuestionHistoryRepository
{
    public UserQuestionHistoryRepository(DataContext context) : base(context)
    {
    }
    // Additional specific methods for UserQuestionHistoryEntity if needed
}
