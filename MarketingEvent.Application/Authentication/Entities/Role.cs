using MarketingEvent.Database.Common.Entities;

namespace MarketingEvent.Database.Authentication.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}
