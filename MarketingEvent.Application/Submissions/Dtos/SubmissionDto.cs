using MarketingEvent.Database.Attachments.Dtos;
using MarketingEvent.Database.Authentication.Dtos;
using MarketingEvent.Database.Comments.Dtos;
using MarketingEvent.Database.Events.Dtos;

namespace MarketingEvent.Database.Submissions.Dtos
{
    public class SubmissionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPublicized { get; set; }
        
        public EventDto Event { get; set; }
        public UserDto CreatedBy { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
