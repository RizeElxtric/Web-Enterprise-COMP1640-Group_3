using MarketingEvent.Database.Authentication.Mappers;
using MarketingEvent.Database.Faculties.Dtos;
using MarketingEvent.Database.Faculties.Entities;
using MarktetingEvent.Database.Faculties.Commands;

namespace MarketingEvent.Database.Faculties.Mappers
{
    public class FacultyMapper
    {
        private readonly UserMapper _userMapper;

        public FacultyMapper(UserMapper userMapper)
        {
            _userMapper = userMapper;
        }
        public async Task<Faculty> ToEntityAsync(CreateFacultyCommand command)
        {
            return new Faculty()
            {
                Id = Guid.NewGuid(),
                FacultyName = command.FacultyName,
                MarketingCoordinatorId = command.MarketingCoordinatorId
            };
        }

        public async Task<Faculty> ToEntityAsync(UpdateFacultyCommand command)
        {
            return new Faculty()
            {
                FacultyName = command.FacultyName,
                MarketingCoordinatorId = command.MarketingCoordinatorId
            };
        }

        public FacultyDto ToDto(Faculty entity)
        {
            if (entity == null)
                return null;
            return new FacultyDto()
            {
                Id = entity.Id,
                FacultyName = entity.FacultyName,
                MarketingCoordinator = _userMapper.ToDto(entity.MarketingCoordinator),
                Users = entity.Users.Select(x => _userMapper.ToDto(x)).ToList()
            };
        }
    }
}
