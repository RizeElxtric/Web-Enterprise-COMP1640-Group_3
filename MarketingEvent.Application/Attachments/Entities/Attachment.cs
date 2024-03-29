using MarketingEvent.Database.Common.Entities;
using MarketingEvent.Database.Submissions.Entities;

namespace MarketingEvent.Database.Attachments.Entities
{
    public class Attachment : BaseEntity
    {
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}
