using System.Linq.Expressions;

namespace QuizApp.Lib.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<bool> CreateAsync(TEntity entity);
    Task<bool> CreateRangeAsync(IEnumerable<TEntity> entities);
    Task<bool> DeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>> ReadAllAsync();
    Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> ReadRangeAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> UpdateAsync(TEntity entity);
}