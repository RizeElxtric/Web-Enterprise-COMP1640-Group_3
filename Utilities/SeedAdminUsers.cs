using MarketingEvent.Api.Services;
using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Authentication.Repositories;
using MarketingEvent.Database.Common;
using MarketingEvent.Database.Constants;

namespace MarketingEvent.Api.Authentication.Utilities
{
    public class SeedAdminUsers
    {
        private readonly ILogger<SeedAdminUsers> _logger;
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SeedAdminUsers(
            ILogger<SeedAdminUsers> logger,
            UserRepository userRepository,
            RoleRepository roleRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync()
        {
            var existingAdmin = await _userRepository.GetAllUserByRoleAsync(RoleConstants.Admin);
            if (!existingAdmin.Any())
            {
                var adminRole = await _roleRepository.GetByRoleNameAsync(RoleConstants.Admin);

                _logger.LogInformation("Seeding admin user");
                var admin = new User()
                {
                    Username = "username",
                    Password = PasswordEncryption.EncryptPassword("username", "password"),
                    Email = "test@email",
                    Roles = new List<Role>() { adminRole }
                };

                await _userRepository.InsertAsync(admin);

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation("Roles already exist, skip seeding roles");
            }
        }
    }
}
