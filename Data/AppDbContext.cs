using Microsoft.EntityFrameworkCore;

namespace WebAppStudent.Data
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Student> Student { get; init; }
        public DbSet<Teachers> Teachers { get; init; }
        public DbSet<Subjects> Subjects { get; init; }
        public DbSet<Marks> Marks { get; init; }
    }
}
