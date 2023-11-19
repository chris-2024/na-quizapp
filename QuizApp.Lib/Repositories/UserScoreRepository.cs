using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class UserScoreRepository : Repository<UserScoreEntity>, IUserScoreRepository
{
    public UserScoreRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for UserScoreEntity if needed
}
