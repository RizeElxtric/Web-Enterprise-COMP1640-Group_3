using MarketingEvent.Database.Common.Repositories;
using MarketingEvent.Database.Context;
using MarketingEvent.Database.Events.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketingEvent.Database.Events.Repositories
{
    public class EventRepository : BaseRepository<Event>
    {
        public EventRepository(MarketingEventDbContext context) : base(context)
        {
        }

        public async Task<List<Event>> GetAllAsyncNoTracking()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public async Task<Event> GetEventDetailAsync(Guid id)
        {
            return await _table.Include(x => x.Submissions.Where(y=>y.IsPublicized))
                    .ThenInclude(x=>x.CreatedBy)
                .FirstOrDefaultAsync();
        }
    }
}
