using MarketingEvent.Api.Authentication.Utilities;
using MarketingEvent.Api.Utilities;
using MarketingEvent.Database.Attachments.Mappers;
using MarketingEvent.Database.Attachments.Repositories;
using MarketingEvent.Database.Authentication.Mappers;
using MarketingEvent.Database.Authentication.Repositories;
using MarketingEvent.Database.Comments.Mappers;
using MarketingEvent.Database.Common;
using MarketingEvent.Database.Events.Mappers;
using MarketingEvent.Database.Events.Repositories;
using MarketingEvent.Database.Faculties.Mappers;
using MarketingEvent.Database.Faculties.Repositories;
using MarketingEvent.Database.Submissions.Mappers;
using MarketingEvent.Database.Submissions.Repositories;

namespace MarketingEvent.Api.Services
{
    public static class ServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddAuthenticationServices();
            serviceCollection.AddFacultyServices();
            serviceCollection.AddEventServices();
            serviceCollection.AddAttachmentServices();
            serviceCollection.AddSubmissionServices();
            serviceCollection.AddCommentServices();

            serviceCollection.AddUtilities();
        }

        public static void AddAuthenticationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<UserRepository>();
            serviceCollection.AddScoped<RoleRepository>();

            serviceCollection.AddScoped<UserMapper>();
        }

        public static void AddFacultyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<FacultyRepository>();

            serviceCollection.AddScoped<FacultyMapper>();
        }
        public static void AddEventServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<EventRepository>();

            serviceCollection.AddScoped<EventMapper>();
        }
        public static void AddAttachmentServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<AttachmentRepository>();

            serviceCollection.AddScoped<AttachmentMapper>();
        }
        public static void AddSubmissionServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SubmissionRepository>();

            serviceCollection.AddScoped<SubmissionMapper>();
        }

        public static void AddCommentServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<CommentMapper>();
        }

        public static void AddUtilities(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<FileHandler>();
            serviceCollection.AddScoped<RandomPasswordGenerator>();
            serviceCollection.AddScoped<SeedRoles>();
            serviceCollection.AddScoped<SeedAdminUsers>();
        }
    }
}
