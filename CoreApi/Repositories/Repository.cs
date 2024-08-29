using EmaAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmaAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EmaDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(EmaDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<T> List()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            return _dbSet.Include(navigationPropertyPath);
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetSortedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool descending = false)
        {
            return descending
                ? await _dbSet.OrderByDescending(keySelector).ToListAsync()
                : await _dbSet.OrderBy(keySelector).ToListAsync();
        }

        public async Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync<TKey>(
            int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, bool descending = false)
        {
            var totalCount = await _dbSet.CountAsync();
            var items = descending
                ? await _dbSet.OrderByDescending(keySelector)
                              .Skip(pageIndex * pageSize)
                              .Take(pageSize)
                              .ToListAsync()
                : await _dbSet.OrderBy(keySelector)
                              .Skip(pageIndex * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
            return (items, totalCount);
        }
    }
}
