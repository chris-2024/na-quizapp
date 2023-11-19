using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;
using System.Diagnostics;
using System.Linq.Expressions;
namespace QuizApp.Lib.Repositories;

public class QuestionRepository : Repository<QuestionEntity>, IQuestionRepository
{
    private readonly DataContext _context;

    public QuestionRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    // Override ReadRange to include answers
    public override async Task<IEnumerable<QuestionEntity>> ReadRangeAsync(Expression<Func<QuestionEntity, bool>> predicate)
    {
        try
        {
            // Use Include to eagerly load the Answers related data
            return await _context.Questions.Include(q => q.Answers)
                               .Where(predicate)
                               .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
}
