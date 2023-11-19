using Microsoft.EntityFrameworkCore;
using QuizApp.Lib.Contexts;
using System.Diagnostics;
using System.Linq.Expressions;

namespace QuizApp.Lib.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected Repository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity ?? null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<bool> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            await _dbSet.AddRangeAsync(entities);
            var result = await _context.SaveChangesAsync();
            return result >= entities.Count();
        }
        catch (DbUpdateException ex)
        {
            foreach (var entry in ex.Entries)
            {
                Debug.WriteLine($"Entity of type {entry.Entity.GetType().Name} in state {entry.State} could not be updated.");
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public virtual async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return (await _dbSet.FirstOrDefaultAsync(predicate))!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> ReadRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException ex)
        {
            Debug.WriteLine($"DbUpdateException: {ex.Message}");
            // Print inner exception details
            var innerException = ex.InnerException;
            while (innerException != null)
            {
                Debug.WriteLine($"Inner Exception: {innerException.Message}");
                innerException = innerException.InnerException;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (DbUpdateException ex)
        {
            Debug.WriteLine($"DbUpdateException: {ex.Message}");
            // Print inner exception details
            var innerException = ex.InnerException;
            while (innerException != null)
            {
                Debug.WriteLine($"Inner Exception: {innerException.Message}");
                innerException = innerException.InnerException;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public virtual async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            _dbSet.RemoveRange(entities);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entities = await _dbSet.Where(predicate).ToListAsync();
            _dbSet.RemoveRange(entities);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
