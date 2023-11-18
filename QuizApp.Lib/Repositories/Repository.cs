using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace QuizApp.Lib.Repositories;

internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<bool> CreateAsync(TEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<bool> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            await _dbSet.AddRangeAsync(entities);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return (await _dbSet.FirstOrDefaultAsync(predicate))!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<TEntity>> ReadRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<TEntity>> ReadAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
