using MarketingEvent.Database.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingEvent.Database.Common.Repositories.Interface
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByListIdAsync(List<Guid> ids);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(Guid id);
    }
}
