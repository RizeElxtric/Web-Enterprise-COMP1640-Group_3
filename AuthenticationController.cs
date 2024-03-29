using MarketingEvent.Api.Helpers;
using MarketingEvent.Api.Services;
using MarketingEvent.Api.Utilities;
using MarketingEvent.Database.Authentication.Mappers;
using MarketingEvent.Database.Authentication.Repositories;
using MarketingEvent.Database.Common;
using MarketingEvent.Database.Constants;
using MarketingEvent.Database.Faculties.Dtos;
using MarketingEvent.Database.Faculties.Mappers;
using MarktetingEvent.Api.Models.Authentication;
using MarktetingEvent.Database.Authentication.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarketingEvent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserMapper _userMapper;
        private readonly FacultyMapper _facultyMapper;
        private readonly RandomPasswordGenerator _rng;
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            UserMapper userMapper,
            FacultyMapper facultyMapper,
            RandomPasswordGenerator rng,
            UserRepository userRepository,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _userMapper = userMapper;
            _facultyMapper = facultyMapper;
            _rng = rng;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get All User
        /// </summary>
        [HttpGet]
        [Route("/get-user")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUserDetailed();
            var dtos = users.Select(x =>
            {
                var dto = _userMapper.ToDto(x);
                dto.Faculty = x.Faculty == null ? null : new FacultyDto()
                {
                    Id = x.Faculty!.Id,
                    FacultyName = x.Faculty!.FacultyName
                };
                return dto;
            }).ToList();

            return Ok(dtos);
        }

        /// <summary>
        /// Get User with Id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("/get-user/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userRepository.GetDetailByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var dto = _userMapper.ToDto(user);
            return Ok(dto);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id in database of created user</returns>
        [HttpPost]
        [Route("/create-account")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var duplicated = await _userRepository.GetByUsernameAsync(command.Username);
            if (duplicated != null)
            {
                return BadRequest("Duplicated username");
            }

            var duplicatedEmail = await _userRepository.GetByEmailAsync(command.Email);
            if (duplicatedEmail != null)
            {
                return BadRequest("Duplicated Email address");
            }

            var entity = await _userMapper.ToEntityAsync(command);

            entity.Password = PasswordEncryption.EncryptPassword(entity.Username, entity.Password);

            await _userRepository.InsertAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return Ok(entity.Id);
        }

        /// <summary>
        /// Change password of currently logged in user
        /// </summary>
        /// <param name="command"></param>
        [HttpPatch]
        [Route("/change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            Guid userId;
            var parsedId = Guid.TryParse(identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out userId);
            if (!parsedId)
            {
                return Forbid();
            }
            var username = identity.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            if (string.IsNullOrEmpty(username))
            {
                return Forbid();
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var encryptedOldPassword = PasswordEncryption.EncryptPassword(username, command.Password);
            if (encryptedOldPassword != user.Password)
            {
                return BadRequest("Wrong password");
            }

            var encryptedNewPassword = PasswordEncryption.EncryptPassword(username, command.NewPassword);
            user.Password = encryptedNewPassword;

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Admin reset password of an user to a randomly generated password
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Plaintext generated password</returns>
        [HttpPatch]
        [Route("/reset-password/{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> ResetPassword(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var password = _rng.GenerateRandomPassword();

            var encryptedNewPassword = PasswordEncryption.EncryptPassword(user.Username, password);
            user.Password = encryptedNewPassword;

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return Ok(password);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns>JWT token</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userRepository.GetByUsernameAsync(model.Username);

            if (user == null)
            {
                return NotFound("Incorrect credential");
            }

            var encryptedPassword = PasswordEncryption.EncryptPassword(model.Username, model.Password);

            if (encryptedPassword == user.Password)
            {
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigurationHelper.Configuration["Jwt:Key"]));
                var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var roles = "";
                if (user.Roles != null && user.Roles.Any())
                {
                    roles = string.Join(",", user.Roles.Select(x => x.RoleName).ToList());
                }
                var claimList = new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, roles)
                };
                var token = new JwtSecurityToken(null, null, claimList, null, DateTime.Now.AddMinutes(60), signingCredential);
                var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(tokenHandler);
            }

            return NotFound("Incorrect credential");
        }
    }
}