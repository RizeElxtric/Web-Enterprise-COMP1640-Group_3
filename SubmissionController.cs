using MarketingEvent.Api.Utilities;
using MarketingEvent.Database.Attachments.Entities;
using MarketingEvent.Database.Attachments.Repositories;
using MarketingEvent.Database.Authentication.Repositories;
using MarketingEvent.Database.Common;
using MarketingEvent.Database.Constants;
using MarketingEvent.Database.Faculties.Repositories;
using MarketingEvent.Database.Submissions.Command;
using MarketingEvent.Database.Submissions.Mappers;
using MarketingEvent.Database.Submissions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketingEvent.Api.Controllers
{
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ILogger<SubmissionController> _logger;
        private readonly FileHandler _fileHandler;
        private readonly FacultyRepository _facultyRepository;
        private readonly AttachmentRepository _attachmentRepository;
        private readonly SubmissionRepository _submissionRepository;
        private readonly UserRepository _userRepository;
        private readonly SubmissionMapper _submissionMapper;
        private readonly IUnitOfWork _unitOfWork;

        public SubmissionController(
            ILogger<SubmissionController> logger,
            FileHandler fileHandler,
            FacultyRepository facultyRepository,
            AttachmentRepository attachmentRepository,
            SubmissionRepository submissionRepository,
            UserRepository userRepository,
            SubmissionMapper submissionMapper,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _fileHandler = fileHandler;
            _facultyRepository = facultyRepository;
            _attachmentRepository = attachmentRepository;
            _submissionRepository = submissionRepository;
            _userRepository = userRepository;
            _submissionMapper = submissionMapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Submit an article to an event. Must be a doc or docx file
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = RoleConstants.Student)]
        [DisableRequestSizeLimit]
        [Route("submission/submit")]
        public async Task<IActionResult> SubmitArticle([FromForm] SubmitSubmissionCommand command)
        {
            var files = Request.Form.Files;
            var id = Guid.NewGuid();
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, ArticleConstants.FileRoot, id.ToString());
            IFormFile file;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            var parsed = Guid.TryParse(identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid userId);
            if (!parsed)
            {
                return Forbid();
            }

            try
            {
                file = files.Single(x => ArticleConstants.ArticleExtensions.Contains(Path.GetExtension(x.FileName)));
            }
            catch
            {
                return Conflict("Must contain exactly 1 Word document file");
            }

            try
            {
                var savedPath = await _fileHandler.SaveFileAsync(file, path);

                var submission = await _submissionMapper.ToEntityAsync(command, userId);
                submission.Path = savedPath;
                submission.Id = id;

                await _submissionRepository.InsertAsync(submission);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }

        /// <summary>
        /// Update an existing article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = RoleConstants.Student)]
        [DisableRequestSizeLimit]
        [Route("submission/submit/{id}")]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromForm] UpdateSubmissionCommand command)
        {
            var files = Request.Form.Files;
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, ArticleConstants.FileRoot, id.ToString());
            IFormFile file;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            var parsed = Guid.TryParse(identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid userId);
            if (!parsed)
            {
                return Forbid();
            }

            var submission = await _submissionRepository.GetDetailSubmissionAsync(id);
            if (submission.CreatedById != userId)
            {
                return Forbid("Submission by other student");
            }

            try
            {
                file = files.Single(x => ArticleConstants.ArticleExtensions.Contains(Path.GetExtension(x.FileName)));
            }
            catch
            {
                return Conflict("Must contain exactly 1 Word document file");
            }

            try
            {
                var savedPath = await _fileHandler.SaveFileAsync(file, path);

                submission.Title = command.Title;
                submission.Path = savedPath;
                submission.IsPublicized = false;

                await _submissionRepository.UpdateAsync(submission);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }

        /// <summary>
        /// Upload an image to an existing article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = RoleConstants.Student)]
        [DisableRequestSizeLimit]
        [Route("submission/{id}/submit-image")]
        public async Task<IActionResult> SubmitImage(Guid id, [FromForm] IFormFileCollection formFile)
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, ArticleConstants.FileRoot, id.ToString());

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            var parsed = Guid.TryParse(identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid userId);
            if (!parsed)
            {
                return Forbid();
            }

            if (formFile.Any(x => !ArticleConstants.ImageExtensions.Contains(Path.GetExtension(x.FileName))))
            {
                return Conflict($"Must contain only image of {string.Join(',', ArticleConstants.ImageExtensions)}");
            }

            var submission = await _submissionRepository.GetDetailSubmissionAsync(id);
            if (submission == null)
            {
                return NotFound("Submission not found");
            }

            try
            {
                foreach (var file in formFile)
                {
                    var savedPath = await _fileHandler.SaveFileAsync(file, path);
                    var attachment = new Attachment()
                    {
                        CreatedDate = DateTime.UtcNow,
                        Description = "Image",
                        SubmissionId = id,
                        Path = savedPath
                    };
                    await _attachmentRepository.InsertAsync(attachment);

                    submission.Attachments.Add(attachment);
                }

                await _submissionRepository.UpdateAsync(submission);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }

        /// <summary>
        /// Get a submission using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("submission/{id}")]
        [Authorize]
        public async Task<IActionResult> GetSubmission(Guid id)
        {
            var submission = await _submissionRepository.GetDetailSubmissionAsync(id);

            if (submission == null)
            {
                return NotFound("Submission not found");
            }

            var dto = _submissionMapper.ToDto(submission);
            return Ok(dto);
        }


        /// <summary>
        /// Get current logged in submission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("submission/my")]
        [Authorize(Roles = RoleConstants.Student)]
        public async Task<IActionResult> GetMySubmission()
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

            var submissions = await _submissionRepository.GetUserSubmissionAsync(userId);

            if (submissions == null || !submissions.Any())
            {
                return NotFound("Submission not found");
            }

            var dtos = submissions.Select(x => _submissionMapper.ToDto(x)).ToList(); ;
            return Ok(dtos);
        }

        /// <summary>
        /// Get all submission of current marketing coordinator's faculty
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("submission")]
        [Authorize(Roles = RoleConstants.MarketingCoordinator)]
        public async Task<IActionResult> GetFacultySubmission()
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

            var coordinator = await _userRepository.GetByIdAsyncNoTracking(userId);
            if (coordinator == null || coordinator.CoordinatorForId == null)
            {
                return BadRequest("Coordinator not found");
            }

            var facultySubmissions = await _submissionRepository.GetAllSubmissionInFacultyAsync(coordinator.CoordinatorForId.Value);
            if (facultySubmissions == null || !facultySubmissions.Any())
            {
                return NotFound("No submision in faculty");
            }

            var dto = facultySubmissions.Select(x => _submissionMapper.ToDto(x)).ToList();

            return Ok(dto);
        }

        /// <summary>
        /// Make a submission public using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("submission/publicize/{id}")]
        [Authorize(Roles = RoleConstants.MarketingCoordinator)]
        public async Task<IActionResult> SelectForPublication(Guid id)
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

            var coordinator = await _userRepository.GetByIdAsyncNoTracking(userId);
            if (coordinator == null || coordinator.CoordinatorForId == null)
            {
                return BadRequest("Coordinator not found");
            }

            var submission = await _submissionRepository.GetDetailSubmissionAsync(id);
            if(submission == null)
            {
                return NotFound("Submission not found");
            }
            if (submission.CreatedBy.FacultyId != coordinator.CoordinatorForId)
            {
                return Forbid("Submission in other faculty");
            }

            submission.IsPublicized = true;
            await _submissionRepository.UpdateAsync(submission);
            await _unitOfWork.SaveChangesAsync();

            return Ok(submission.Id);
        }

        /// <summary>
        /// Get all submission
        /// </summary>
        /// <param name="id">Faculty ID, if presented, get all submission within this faculty. Else, get all submission from all faculties</param>
        /// <returns></returns>
        [HttpGet]
        [Route("submission/get-all-submission/{id?}")]
        [Authorize(Roles = RoleConstants.MarketingManager)]
        public async Task<IActionResult> GetAllSubmission(Guid? id)
        {
            if (id != null)
            {
                var faculty = await _facultyRepository.GetByIdAsyncNoTracking(id.Value);
                if (faculty == null)
                {
                    return NotFound("Faculty not found");
                }

                var facultySubmissions = await _submissionRepository.GetAllSubmissionInFacultyAsync(id.Value);
                if (facultySubmissions == null || !facultySubmissions.Any())
                {
                    return NotFound("No submision in faculty");
                }

                var dto = facultySubmissions.Select(x => _submissionMapper.ToDto(x)).ToList();

                return Ok(dto);
            }

            var allSubmissions = await _submissionRepository.GetAllSubmissionAsync();
            if (allSubmissions == null || !allSubmissions.Any())
            {
                return NotFound("No submission");
            }

            var allDto = allSubmissions.Select(x => _submissionMapper.ToDto(x)).ToList();

            return Ok(allDto);
        }

        [HttpGet]
        [Route("cdn/{path}")]
        public async Task<FileStreamResult> GetFiles(string path)
        {
            if (path.Contains("../"))
            {
                return null;
            }

            var root = Directory.GetCurrentDirectory();
            var filePath = root + path;

            var file = System.IO.File.OpenRead(filePath);

            return new FileStreamResult(file, "application/octet-stream");
        }
    }
}