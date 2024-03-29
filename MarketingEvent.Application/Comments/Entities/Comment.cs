using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Common.Entities;
using MarketingEvent.Database.Submissions.Entities;

namespace MarketingEvent.Database.Comments.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid CommentById { get; set; }
        public User CommentBy { get; set; }
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}