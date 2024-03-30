namespace MarketingEvent.Api.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _config;
        public static void Initialize(IConfiguration Configuration)
        {
            _config = Configuration;
        }

        public static IConfiguration Configuration { get { return _config; } }
    }
}
