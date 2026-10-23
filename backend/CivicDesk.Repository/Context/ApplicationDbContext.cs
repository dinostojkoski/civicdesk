using CivicDesk.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CivicDesk.Repository.Context;

public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    {
    }
    
    public DbSet<City> Cities => Set<City>();
    public DbSet<Municipality> Municipalities => Set<Municipality>();
    public DbSet<Institution> Institutions => Set<Institution>();

    public DbSet<InstitutionMembership> InstitutionMemberships
        => Set<InstitutionMembership>();

    public DbSet<InstitutionRegistrationRequest> InstitutionRegistrationRequests
        => Set<InstitutionRegistrationRequest>();

    public DbSet<Report> Reports => Set<Report>();
    public DbSet<ReportCategory> ReportCategories => Set<ReportCategory>();
    public DbSet<ReportAttachment> ReportAttachments => Set<ReportAttachment>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}