using MarketingEvent.Database.Events.Dtos;
using MarketingEvent.Database.Events.Entities;
using MarktetingEvent.Database.Events.Commands;

namespace MarketingEvent.Database.Events.Mappers
{
    public class EventMapper
    {
        public async Task<Event> ToEntityAsync(CreateEventCommand command, Guid currentUser)
        {
            return new Event()
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                FirstClosureDate = command.FirstClosureDate,
                FinalClosureDate = command.FinalClosureDate,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CreatedById = currentUser,
                UpdatedById = currentUser
            };
        }

        public async Task<Event> ToEntityAsync(UpdateEventCommand command, Guid currentUser)
        {
            return new Event()
            {
                Name = command.Name,
                Description = command.Description,
                FirstClosureDate = command.FirstClosureDate,
                FinalClosureDate = command.FinalClosureDate,
                UpdatedOn = DateTime.UtcNow,
                UpdatedById = currentUser
            };
        }

        public EventDto ToDto(Event entity)
        {
            return new EventDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                FirstClosureDate = entity.FirstClosureDate,
                FinalClosureDate = entity.FinalClosureDate
            };
        }

        public EventDetailDto ToDetailDto(Event entity)
        {
            return new EventDetailDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                FirstClosureDate = entity.FirstClosureDate,
                FinalClosureDate = entity.FinalClosureDate
            };
        }
    }
}
