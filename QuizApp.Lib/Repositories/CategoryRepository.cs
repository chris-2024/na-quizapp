using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;
namespace QuizApp.Lib.Repositories;

public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(DataContext context) : base(context)
    {
    }
    // Additional specific methods for CategoryEntity if needed
}
