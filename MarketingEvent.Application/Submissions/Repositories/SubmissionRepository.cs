using MarketingEvent.Database.Common.Repositories;
using MarketingEvent.Database.Context;
using MarketingEvent.Database.Submissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketingEvent.Database.Submissions.Repositories
{
    public class SubmissionRepository : BaseRepository<Submission>
    {
        public SubmissionRepository(MarketingEventDbContext context) : base(context)
        {
        }

        public async Task<Submission> GetDetailSubmissionAsync(Guid id)
        {
            return await _table.Include(x => x.Attachments)
                .Include(x => x.CreatedBy)
                .Include(x => x.Comments)
                    .ThenInclude(x => x.CommentBy)
                .Include(x=>x.Event)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Submission>> GetAllSubmissionAsync()
        {
            return await _table
                .Include(x => x.CreatedBy)
                    .ThenInclude(x=>x.Faculty)
                .Include(x=>x.Event)
                .ToListAsync();
        }

        public async Task<List<Submission>> GetAllSubmissionInFacultyAsync(Guid facultyId)
        {
            return await _table
                .Include(x => x.CreatedBy)
                .Include(x => x.Event)
                .Where(x => x.CreatedBy.FacultyId == facultyId)
                .ToListAsync();
        }

        public async Task<List<Submission>> GetUserSubmissionAsync(Guid userId)
        {
            return await _table
                .Include(x=>x.CreatedBy)
                .Include(x => x.Event)
                .Where(x => x.CreatedById == userId)
                .ToListAsync();
        }
    }
}
