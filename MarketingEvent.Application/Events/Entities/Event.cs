using MarketingEvent.Database.Common.Entities;
using MarketingEvent.Database.Submissions.Entities;

namespace MarketingEvent.Database.Events.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FirstClosureDate { get; set; }
        public DateTime? FinalClosureDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }

        public List<Submission> Submissions { get; set; }
    }
}