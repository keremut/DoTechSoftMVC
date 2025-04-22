using DoTechSoftMVC.Data.Concrete.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoTechSoftMVC.Data.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, string>
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }

        public DbSet<WhyUs> WhyUs { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<About> Abouts { get; set; } = null!;
        public DbSet<Home> Homes { get; set; } = null!;
        public DbSet<Staff> Staffs { get; set; } = null!;
        public DbSet<Subscribe> Subscribes { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
    }
}
