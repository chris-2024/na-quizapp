using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class UserScoreRepository : Repository<UserScoreEntity>, IUserScoreRepository
{
    public UserScoreRepository(DataContext context) : base(context)
    {
    }
    // Additional specific methods for UserScoreEntity if needed
}
