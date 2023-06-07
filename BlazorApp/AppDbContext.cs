using Microsoft.EntityFrameworkCore;

namespace BlazorApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Task> task { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}