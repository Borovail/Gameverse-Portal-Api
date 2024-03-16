using Back_End.Models.BugModes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Data
{
    public class ApiDbContext : IdentityDbContext<IdentityUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BugModel>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()"); // For SQL Server


            //uncomment  when  authentication is implemented
            //modelBuilder.Entity<BugModel>()
            //        .HasOne(b => b.User)
            //         .WithMany() 
            //           .HasForeignKey(b => b.UserId);
        }

        public DbSet<BugModel> Bugs { get; set; }

    }
}
