using MarketingEvent.Database.Authentication.Dtos;

namespace MarketingEvent.Database.Comments.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDto CommentBy { get; set; }
    }
}
