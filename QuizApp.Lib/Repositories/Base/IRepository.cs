using System.Linq.Expressions;

namespace QuizApp.Lib.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<bool> CreateRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> ReadRangeAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> ReadAllAsync();
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);

}