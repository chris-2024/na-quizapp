using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class UserQuestionHistoryRepository : Repository<UserQuestionHistoryEntity>, IUserQuestionHistoryRepository
{
    public UserQuestionHistoryRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for UserQuestionHistoryEntity if needed
}
