using MarketingEvent.Database.Authentication.Dtos;
using MarketingEvent.Database.Authentication.Entities;

namespace MarketingEvent.Database.Faculties.Dtos
{
    public class FacultyDto
    {
        public Guid Id { get; set; }
        public string FacultyName { get; set; }
        public UserDto MarketingCoordinator { get; set; }
        public List<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
