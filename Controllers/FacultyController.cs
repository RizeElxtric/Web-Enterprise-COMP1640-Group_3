using MarketingEvent.Database.Authentication.Repositories;
using MarketingEvent.Database.Common;
using MarketingEvent.Database.Constants;
using MarketingEvent.Database.Faculties.Mappers;
using MarketingEvent.Database.Faculties.Repositories;
using MarktetingEvent.Database.Faculties.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketingEvent.Api.Controllers
{
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly ILogger<FacultyController> _logger;
        private readonly FacultyMapper _facultyMapper;
        private readonly FacultyRepository _facultyRepository;
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FacultyController(
            ILogger<FacultyController> logger,
            FacultyMapper facultyMapper,
            FacultyRepository facultyRepository,
            UserRepository userRepository,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _facultyMapper = facultyMapper;
            _facultyRepository = facultyRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create new faculty
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id of created faculty</returns>
        [HttpPost]
        [Route("/faculty")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> CreateFacultyAsync([FromBody] CreateFacultyCommand command)
        {
            var coordinator = await _userRepository.GetDetailByIdAsync(command.MarketingCoordinatorId);
            if (coordinator == null)
            {
                return NotFound("Marketing coordinator not found");
            }

            if (coordinator.CoordinatorForId != null)
            {
                return Conflict("Marketing coordinator already in charge of other faculty");
            }

            if (coordinator.FacultyId != null)
            {
                return Conflict("User is in other faculty");
            }

            if(!coordinator.Roles.Any(x=>x.RoleName == RoleConstants.MarketingCoordinator))
            {
                return Conflict("User is not marketing coordinator");
            }

            var entity = await _facultyMapper.ToEntityAsync(command);

            await _facultyRepository.InsertAsync(entity);

            coordinator.CoordinatorForId = entity.Id;
            coordinator.FacultyId = entity.Id;
            await _userRepository.UpdateAsync(coordinator);

            await _unitOfWork.SaveChangesAsync();

            return Ok(entity.Id);
        }

        /// <summary>
        /// Update an existing faculty
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        [HttpPut]
        [Route("/faculty/{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateFacultyAsync(Guid id, [FromBody] UpdateFacultyCommand command)
        {
            var faculty = await _facultyRepository.GetByIdAsyncNoTracking(id);
            if (faculty == null)
            {
                return NotFound("Faculty not found");
            }

            var coordinator = await _userRepository.GetDetailByIdAsync(command.MarketingCoordinatorId);
            if (coordinator == null)
            {
                return NotFound("Marketing coordinator not found");
            }

            if (coordinator.CoordinatorForId != null && coordinator.Id != command.MarketingCoordinatorId)
            {
                return Conflict("Marketing coordinator already in charge of other faculty");
            }

            if (coordinator.FacultyId != null && coordinator.FacultyId != faculty.Id)
            {
                return Conflict("User is in other faculty");
            }

            if (!coordinator.Roles.Any(x => x.RoleName == RoleConstants.MarketingCoordinator))
            {
                return Conflict("User is not marketing coordinator");
            }

            if (faculty.MarketingCoordinatorId != null && faculty.MarketingCoordinatorId != command.MarketingCoordinatorId)
            {
                var oldCoordinator = await _userRepository.GetByIdAsync(faculty.MarketingCoordinatorId);
                oldCoordinator.CoordinatorForId = null;
                await _userRepository.UpdateAsync(oldCoordinator);
            }

            var entity = await _facultyMapper.ToEntityAsync(command);
            entity.Id = id;

            coordinator.CoordinatorForId = id;

            await _facultyRepository.UpdateAsync(entity);
            await _userRepository.UpdateAsync(coordinator);

            await _unitOfWork.SaveChangesAsync();

            return Ok(entity.Id);
        }

        /// <summary>
        /// Get all faculties
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/faculty")]
        [Authorize(Roles =RoleConstants.Admin)]
        public async Task<IActionResult> GetAllFacultiesAsync()
        {
            var faculties = await _facultyRepository.GetAllFacultiesAsync();
            var dtos = faculties.Select(x => _facultyMapper.ToDto(x)).ToList();

            return Ok(dtos);
        }

        /// <summary>
        /// Get a faculty using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/faculty/{id}")]
        public async Task<IActionResult> GetFacultyAsync(Guid id)
        {
            var faculty = await _facultyRepository.GetFacultyDetailAsync(id);
            if (faculty == null)
            {
                return NotFound("Faculty not found");
            }

            var dto = _facultyMapper.ToDto(faculty);

            return Ok(dto);
        }

        /// <summary>
        /// Delete a faculty
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/faculty/{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteFacultyAsync(Guid id)
        {
            var faculty = await _facultyRepository.GetByIdAsync(id);
            if (faculty == null)
            {
                return NotFound("Faculty not found");
            }

            var users = await _userRepository.GetAllUserByFacultyAsync(id);

            users.ForEach(async x =>
            {
                x.FacultyId = null;
                x.CoordinatorForId = null;
                await _userRepository.UpdateAsync(x);
            });

            await _facultyRepository.DeleteByIdAsync(id);

            await _unitOfWork.SaveChangesAsync();

            return Ok(id);
        }

        /// <summary>
        /// Assign user to a faculty
        /// </summary>
        /// <param name="id">Faculty Id</param>
        /// <param name="userIds">List of user ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("/faculty/{id}/assign-user")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> AssignUsersToFacultyAsync(Guid id, [FromBody] List<Guid> userIds)
        {
            var users = await _userRepository.GetByListIdAsync(userIds);
            if (users == null)
            {
                return NotFound("No user found");
            }

            var foundIds = users.Select(x => x.Id).ToList();
            var notFound = userIds.Where(x => !foundIds.Contains(x));
            if (notFound.Any())
            {
                return NotFound(notFound.ToList());
            }

            var faculty = await _facultyRepository.GetByIdAsync(id);
            if (faculty == null)
            {
                return NotFound("Faculty not found");
            }

            users.ForEach(async x =>
            {
                x.FacultyId = id;
                await _userRepository.UpdateAsync(x);
            });

            await _unitOfWork.SaveChangesAsync();
            return Ok(users.Select(x => x.Id).ToList());
        }
    }
}