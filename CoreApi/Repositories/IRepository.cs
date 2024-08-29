using System.Linq.Expressions;

namespace EmaAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);
        T Find(int id);
        Task<T> FindAsync(int id);
        IEnumerable<T> List();
        Task<IEnumerable<T>> ListAsync();
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Delete(int id);
        Task DeleteAsync(int id);
        void RemoveRange(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath);
        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetSortedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool descending = false);
        Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync<TKey>(
            int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, bool descending = false);
    }
}
