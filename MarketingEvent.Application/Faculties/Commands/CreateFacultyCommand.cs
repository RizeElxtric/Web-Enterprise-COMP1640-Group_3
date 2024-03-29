namespace MarktetingEvent.Database.Faculties.Commands
{
    public class CreateFacultyCommand
    {
        public string FacultyName { get; set; }
        public Guid MarketingCoordinatorId { get; set; }
    }
}
