using Microsoft.AspNetCore.Http;

namespace MarketingEvent.Database.Submissions.Command
{
    public class UpdateSubmissionCommand
    {
        public string Title { get; set; }
        public IFormFile article { get; set; }
    }
}
