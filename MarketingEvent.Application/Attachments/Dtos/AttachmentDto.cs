namespace MarketingEvent.Database.Attachments.Dtos
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
