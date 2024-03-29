namespace MarketingEvent.Database.Events.Dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FirstClosureDate { get; set; }
        public DateTime? FinalClosureDate { get; set; }
    }
}
