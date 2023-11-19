using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(DbContext context) : base(context)
    {
    }
    // Additional specific methods for CategoryEntity if needed
}
