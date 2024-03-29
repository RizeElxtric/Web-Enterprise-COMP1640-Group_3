using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Common.Repositories;
using MarketingEvent.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace MarketingEvent.Database.Authentication.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(MarketingEventDbContext context) : base(context) { }

        public async Task<List<Role>> GetListByIdAsync(List<Guid> ids)
        {
            return await _table.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<Role>> GetListByRoleNameAsync(List<string> roles)
        {
            return await _table.Where(x => roles.Contains(x.RoleName)).ToListAsync();
        }

        public async Task<Role> GetByRoleNameAsync(string roles)
        {
            return await _table.FirstOrDefaultAsync(x => x.RoleName == roles);
        }
    }
}
