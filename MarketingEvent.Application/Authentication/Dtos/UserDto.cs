using MarketingEvent.Database.Faculties.Dtos;

namespace MarketingEvent.Database.Authentication.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public FacultyDto Faculty { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}