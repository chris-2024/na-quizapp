using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class DifficultyRepository : Repository<DifficultyEntity>, IDifficultyRepository
{
    public DifficultyRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for DifficultyEntity if needed
}
