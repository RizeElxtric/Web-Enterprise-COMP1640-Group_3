using MarketingEvent.Database.Attachments.Entities;
using MarketingEvent.Database.Authentication.Entities;
using MarketingEvent.Database.Comments.Entities;
using MarketingEvent.Database.Events.Entities;
using MarketingEvent.Database.Faculties.Entities;
using MarketingEvent.Database.Submissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketingEvent.Database.Context
{
    public class MarketingEventDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        public MarketingEventDbContext()
        {

        }
        public MarketingEventDbContext(DbContextOptions<MarketingEventDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Faculty)
                .WithMany(f => f.Users)
                .HasForeignKey(u => u.FacultyId)
                .IsRequired(false);

            modelBuilder.Entity<Faculty>()
                .HasOne(f => f.MarketingCoordinator)
                .WithOne(u => u.CoordinatorFor)
                .HasForeignKey<Faculty>(f => f.MarketingCoordinatorId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.CoordinatorFor)
                .WithOne(f => f.MarketingCoordinator)
                .HasForeignKey<User>(u => u.CoordinatorForId)
                .IsRequired(false);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.CommentBy)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.CommentById)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            //=> options.UseSqlServer("Data Source=localhost;Initial Catalog=MarketingEvent;Integrated Security=True;TrustServerCertificate=True");
            => options.UseSqlServer("Data Source=marketingeventgroup4.database.windows.net;Initial Catalog=MarketingEvent;User ID=group4admin;Password=Group4P@ssw0rd");
    }
}
