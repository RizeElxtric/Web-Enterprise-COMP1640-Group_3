using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Common.Entities;

namespace MarketingEvent.Database.Faculties.Entities
{
    public class Faculty : BaseEntity
    {
        public string FacultyName { get; set; }

        public Guid MarketingCoordinatorId { get; set; }
        public User MarketingCoordinator { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
