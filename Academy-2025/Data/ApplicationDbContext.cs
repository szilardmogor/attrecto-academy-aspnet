using Microsoft.EntityFrameworkCore;

namespace Academy_2025.Data
{
    public class ApplicationDbContext : DbContext

    {
        private string DbPath;


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasOne(course => course.Author)
                .WithMany()
                .HasForeignKey(course => course.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
