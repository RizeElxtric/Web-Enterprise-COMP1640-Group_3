namespace MarktetingEvent.Database.Events.Commands
{
    public class UpdateEventCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FirstClosureDate { get; set; }
        public DateTime? FinalClosureDate { get; set; }
    }
}