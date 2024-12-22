using HyundaiTestDriveBooking.Models;
using Microsoft.EntityFrameworkCore;


namespace HyundaiTestDriveBooking.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<TestDriveBooking> TestDriveBookings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
