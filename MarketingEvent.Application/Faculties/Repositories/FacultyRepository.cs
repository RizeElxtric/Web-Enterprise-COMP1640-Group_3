using MarketingEvent.Database.Common.Repositories;
using MarketingEvent.Database.Context;
using MarketingEvent.Database.Faculties.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketingEvent.Database.Faculties.Repositories
{
    public class FacultyRepository : BaseRepository<Faculty>
    {
        public FacultyRepository(MarketingEventDbContext context) : base(context)
        {
        }

        public async Task<Faculty> GetFacultyDetailAsync(Guid Id)
        {
            return await _table.Include(x => x.MarketingCoordinator)
                .Include(x => x.Users)
                    .ThenInclude(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Faculty>> GetAllFacultiesAsync()
        {
            return await _table.Include(x => x.MarketingCoordinator)
                .ToListAsync();
        }
    }
}
