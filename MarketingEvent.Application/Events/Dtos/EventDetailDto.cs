using MarketingEvent.Database.Submissions.Dtos;

namespace MarketingEvent.Database.Events.Dtos
{
    public class EventDetailDto : EventDto
    {
        public List<SubmissionDto> Submissions { get; set; }
    }
}
