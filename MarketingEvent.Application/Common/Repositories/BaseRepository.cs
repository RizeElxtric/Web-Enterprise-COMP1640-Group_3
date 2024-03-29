using MarketingEvent.Database.Common.Entities;
using MarketingEvent.Database.Common.Repositories.Interface;
using MarketingEvent.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingEvent.Database.Common.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected MarketingEventDbContext _context;
        protected DbSet<T> _table;
        public BaseRepository(MarketingEventDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<T> GetByIdAsyncNoTracking(Guid id)
        {
            return await _table.AsNoTracking().FirstAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task InsertRangeAsync(List<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            _table.Remove(entity);
        }

        public async Task<List<T>> GetByListIdAsync(List<Guid> ids)
        {
            return await _table.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
