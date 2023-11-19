using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;

namespace QuizApp.Lib.Repositories;

public class LanguageRepository : Repository<LanguageEntity>, ILanguageRepository
{
    public LanguageRepository(DataContext context) : base(context)
    {
    }
    // Additional specific methods for LanguageEntity if needed
}
