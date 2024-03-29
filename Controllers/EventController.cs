c:\Users\ASUS\Desktop\MarketingEvent-master\MarketingEvent.Api\Controllers\SubmissionController.cs c:\Users\ASUS\Desktop\MarketingEvent-master\MarketingEvent.Api\Controllers\FacultyController.csusing MarketingEvent.Database.Authentication.Repositories;
using MarketingEvent.Database.Common;
using MarketingEvent.Database.Constants;
using MarketingEvent.Database.Events.Mappers;
using MarketingEvent.Database.Events.Repositories;
using MarketingEvent.Database.Submissions.Mappers;
using MarktetingEvent.Database.Events.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketingEvent.Api.Controllers
{
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventMapper _eventMapper;
        private readonly EventRepository _eventRepository;
        private readonly SubmissionMapper _submissionMapper;
        private readonly IUnitOfWork _unitOfWork;

        public EventController(
            ILogger<EventController> logger,
            EventMapper eventMapper,
            EventRepository eventRepository,
            UserRepository userRepository,
            SubmissionMapper submissionMapper,
            IUnitOfWork unitOfWork
            )
        {
            _eventMapper = eventMapper;
            _eventRepository = eventRepository;
            _submissionMapper = submissionMapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create new event
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id in database of created event</returns>
        [HttpPost]
        [Route("/event")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> CreateEventAsync([FromBody] CreateEventCommand command)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            Guid userId;
            var parsed = Guid.TryParse(identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out userId);
            if (!parsed)
            {
                return Forbid();
            }

            var entity = await _eventMapper.ToEntityAsync(command, userId);

            await _eventRepository.InsertAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return Ok(entity.Id);
        }

        /// <summary>
        /// Update an existing event using id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>Id of the event if update successfully</returns>
        [HttpPut]
        [Route("/event/{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateEventAsync(Guid id, [FromBody] UpdateEventCommand command)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            Guid userId;
            var parsed = Guid.TryParse(identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out userId);
            if (!parsed)
            {
                return Forbid();
            }

            var @event = await _eventRepository.GetByIdAsyncNoTracking(id);
            if (@event == null)
            {
                return NotFound("Event not found");
            }

            var entity = await _eventMapper.ToEntityAsync(command, userId);
            entity.Id = @event.Id;

            await _eventRepository.UpdateAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return Ok(entity.Id);
        }

        /// <summary>
        /// Get all events
        /// </summary>
        [HttpGet]
        [Route("/event")]
        [Authorize]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventRepository.GetAllAsyncNoTracking();

            var dto = events.Select(x => _eventMapper.ToDto(x)).ToList();
            return Ok(dto);
        }

        /// <summary>
        /// Get an event using its ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("/event/{id}")]
        [Authorize]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var @event = await _eventRepository.GetEventDetailAsync(id);

            var dto = _eventMapper.ToDetailDto(@event);
            dto.Submissions = @event.Submissions.Select(x=>_submissionMapper.ToDto(x)).ToList();

            return Ok(dto);
        }
    }
}