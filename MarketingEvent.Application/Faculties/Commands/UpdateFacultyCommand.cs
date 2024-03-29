namespace MarktetingEvent.Database.Faculties.Commands
{
    public class UpdateFacultyCommand
    {
        public string FacultyName { get; set; }
        public Guid MarketingCoordinatorId { get; set; }
    }
}
