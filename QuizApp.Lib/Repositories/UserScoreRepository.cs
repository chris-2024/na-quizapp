using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using QuizApp.Lib.Models.Entities;
using System.Diagnostics;
namespace QuizApp.Lib.Repositories;

public class UserScoreRepository : Repository<UserScoreEntity>, IUserScoreRepository
{
    private readonly DataContext _context;

    public UserScoreRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<UserScoreEntity>> ReadAllAsync()
    {
        try
        {
            return await _context.UserScores.Include(us => us.User).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}