using MarketingEvent.Database.Authentication.Dtos;
using MarketingEvent.Database.Authentication.Entities;

namespace MarketingEvent.Database.Authentication.Mappers
{
    public static class RoleMapper
    {
        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto()
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }
    }
}
