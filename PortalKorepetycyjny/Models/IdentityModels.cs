using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PortalKorepetycyjny.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CoachReview> CoachReviews { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Status> Statuses{ get; set; }
        public DbSet<StudentAdvertisment> StudentAdvertisments{ get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public System.Data.Entity.DbSet<PortalKorepetycyjny.Models.Advertisment> Advertisments { get; set; }
    }
}