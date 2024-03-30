using MarketingEvent.Api.Authentication.Utilities;

namespace MarketingEvent.Api.Services
{
    public static class SeedData
    {
        public static async Task ExecuteAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var _seedRole = scope.ServiceProvider.GetRequiredService<SeedRoles>();
            await _seedRole.ExecuteAsync();

            var _seedAdminUser = scope.ServiceProvider.GetRequiredService<SeedAdminUsers>();
            await _seedAdminUser.ExecuteAsync();
        }
    }
}
