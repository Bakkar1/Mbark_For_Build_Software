using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Model;

namespace DataAccessLayer.Data;

public class AppDbContext : IdentityDbContext<Employee>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :
    base(options)
    {}

    public DbSet<ConstructionSite> ConstructionSites { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ConstructionSiteEmployee> ConstructionSiteEmployees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConstructionSiteEmployee>()
            .HasKey(cse => new { cse.ConstructionSiteId, cse.EmployeeId });

        modelBuilder.Entity<ConstructionSiteEmployee>()
            .HasOne(cse => cse.ConstructionSite)
            .WithMany(cs => cs.ConstructionSiteEmployees)
            .HasForeignKey(cse => cse.ConstructionSiteId);

        modelBuilder.Entity<ConstructionSiteEmployee>()
            .HasOne(cse => cse.Employee)
            .WithMany(e => e.ConstructionSiteEmployees)
            .HasForeignKey(cse => cse.EmployeeId);

        modelBuilder.Entity<ConstructionSiteEmployee>()
        .HasOne(cse => cse.Employee)
        .WithMany(e => e.ConstructionSiteEmployees)
        .HasForeignKey(cse => cse.EmployeeId);

        base.OnModelCreating(modelBuilder);
    }

}
