using Microsoft.EntityFrameworkCore;

namespace Uni;

public class UniDbContext(DbContextOptions<UniDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students {get; set;}

    public DbSet<Semester> Semesters {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Semesters) 
            .WithOne(s => s.Student)
            .HasForeignKey(s => s.StudentId);
    }
}
