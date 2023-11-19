using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class LanguageRepository : Repository<LanguageEntity>, ILanguageRepository
{
    public LanguageRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for LanguageEntity if needed
}
