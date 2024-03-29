using MarketingEvent.Database.Authentication.Dtos;
using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Authentication.Repositories;
using MarktetingEvent.Database.Authentication.Commands;

namespace MarketingEvent.Database.Authentication.Mappers
{
    public class UserMapper
    {
        private readonly RoleRepository _roleRepository;

        public UserMapper(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public UserDto ToDto(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Roles = user.Roles.Select(x => x.ToDto()).ToList()
            };
        }

        public async Task<User> ToEntityAsync(CreateAccountCommand user)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                Roles = await _roleRepository.GetListByRoleNameAsync(user.RoleNames)
            };
        }
    }
}
