using CoreApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // IHttpContextAccessor için gerekli
using System.Linq.Expressions;
using CoreApi.Core;

namespace CoreApi.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EmaDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Repository(EmaDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
            if (int.TryParse(userIdString, out var userId))
            {
                return userId;
            }
            // Eğer UserId bulunamazsa veya geçersizse varsayılan bir değer döndür
            return 0;
        }


        public T Add(T entity)
        {
            if (entity is EntityBase entityBase)
            {
                entityBase.UserId = GetUserId();
                entityBase.CreatedDatetime = DateTime.UtcNow;
            }

            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity is EntityBase entityBase)
            {
                entityBase.UserId = GetUserId();
                entityBase.CreatedDatetime = DateTime.UtcNow;
            }

            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity is EntityBase entityBase)
            {
                entityBase.UserId = GetUserId();
                entityBase.UpdatedDatetime = DateTime.UtcNow;
            }

            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity is EntityBase entityBase)
            {
                entityBase.UserId = GetUserId();
                entityBase.UpdatedDatetime = DateTime.UtcNow;
            }

            _dbSet.Update(entity);
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
