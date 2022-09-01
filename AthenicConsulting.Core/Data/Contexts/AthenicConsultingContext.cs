using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AthenicConsulting.Core.Data.Contexts
{
    public class AthenicConsultingContext : IdentityDbContext<ApplicationUser>
    {
        public AthenicConsultingContext(DbContextOptions<AthenicConsultingContext> options) : base(options)
        {

        }
        public AthenicConsultingContext()
        {

        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
