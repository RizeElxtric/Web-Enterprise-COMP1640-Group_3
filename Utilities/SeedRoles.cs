using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Authentication.Repositories;
using MarketingEvent.Database.Common;
using MarketingEvent.Database.Constants;

namespace MarketingEvent.Api.Authentication.Utilities
{
    public class SeedRoles
    {
        private readonly ILogger<SeedRoles> _logger;
        private readonly RoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SeedRoles(
            ILogger<SeedRoles> logger,
            RoleRepository roleRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync()
        {
            var existingRole = await _roleRepository.GetAllAsync();
            if (!existingRole.Any())
            {
                _logger.LogInformation("Seeding roles");
                var defaultRoles = new List<Role>()
                {
                    new Role()
                    {
                        Id = Guid.NewGuid(),
                        RoleName = RoleConstants.Guest
                    },
                    new Role()
                    {
                        Id = Guid.NewGuid(),
                        RoleName = RoleConstants.Student
                    },
                    new Role()
                    {
                        Id = Guid.NewGuid(),
                        RoleName = RoleConstants.MarketingCoordinator
                    },
                    new Role()
                    {
                        Id = Guid.NewGuid(),
                        RoleName = RoleConstants.MarketingManager
                    },
                    new Role()
                    {
                        Id = Guid.NewGuid(),
                        RoleName = RoleConstants.Admin
                    }
                };

                await _roleRepository.InsertRangeAsync(defaultRoles);

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation("Roles already exist, skip seeding roles");
            }
        }
    }
}
