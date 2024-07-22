using JobFit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobFit.Persistence.DataContext;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Recruiter> Recruiters => Set<Recruiter>();
    public DbSet<SkillSet> SkillSets => Set<SkillSet>();
    

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}