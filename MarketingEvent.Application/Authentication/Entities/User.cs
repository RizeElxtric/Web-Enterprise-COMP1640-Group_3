using MarketingEvent.Database.Comments.Entities;
using MarketingEvent.Database.Common.Entities;
using MarketingEvent.Database.Faculties.Entities;

namespace MarketingEvent.Database.Authentication.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
        public Guid? FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
        public List<Comment> Comments { get; set; }
        public Guid? CoordinatorForId { get; set; }
        public Faculty? CoordinatorFor { get; set; }
    }
}
