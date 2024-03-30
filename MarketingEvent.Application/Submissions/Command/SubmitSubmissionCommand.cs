using Microsoft.AspNetCore.Http;

namespace MarketingEvent.Database.Submissions.Command
{
    public class SubmitSubmissionCommand
    {
        public string Title { get; set; }
        public Guid EventId { get; set; }
        public IFormFile article { get; set; }
    }
}
