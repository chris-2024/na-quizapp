using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Repositories;

public class DifficultyRepository : Repository<DifficultyEntity>, IDifficultyRepository
{
    public DifficultyRepository(DataContext context) : base(context)
    {
    }
    // Additional specific methods for DifficultyEntity if needed
}
