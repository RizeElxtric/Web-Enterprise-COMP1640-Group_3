using MarketingEvent.Database.Attachments.Entities;
using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Comments.Entities;
using MarketingEvent.Database.Common.Entities;
using MarketingEvent.Database.Events.Entities;

namespace MarketingEvent.Database.Submissions.Entities
{
    public class Submission : BaseEntity
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPublicized { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
