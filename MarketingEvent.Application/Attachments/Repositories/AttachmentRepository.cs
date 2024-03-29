using MarketingEvent.Database.Attachments.Entities;
using MarketingEvent.Database.Common.Repositories;
using MarketingEvent.Database.Context;

namespace MarketingEvent.Database.Attachments.Repositories
{
    public class AttachmentRepository : BaseRepository<Attachment>
    {
        public AttachmentRepository(MarketingEventDbContext context) : base(context)
        {
        }
    }
}
