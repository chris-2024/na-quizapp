using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class UserRoleRepository : Repository<UserRoleEntity>, IUserRoleRepository
{
    public UserRoleRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for UserRoleEntity if needed
}
