using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Common.Repositories;
using MarketingEvent.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace MarketingEvent.Database.Authentication.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(MarketingEventDbContext context) : base(context) { }

        public async Task<User> GetDetailByIdAsync(Guid id)
        {
            return await _table.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _table.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _table.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<User>> GetAllUserByRoleAsync(string roles)
        {
            return await _table.Include(x => x.Roles).Where(x => x.Roles.Any(x => x.RoleName == roles)).ToListAsync();
        }

        public async Task<List<User>> GetAllUserByFacultyAsync(Guid facultyId)
        {
            return await _table.Where(x=>x.FacultyId == facultyId).ToListAsync();
        }

        public async Task<List<User>> GetAllUserDetailed()
        {
            return await _table.Include(x => x.Roles).Include(x=>x.Faculty).ToListAsync();
        }
    }
}
