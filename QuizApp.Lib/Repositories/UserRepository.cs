using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class UserRepository : Repository<UserEntity>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
    // Additional specific methods for UserEntity if needed
}