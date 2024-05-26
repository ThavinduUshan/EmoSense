using Microsoft.EntityFrameworkCore;

namespace api;

public class DataContext : DbContext
{
    public DbSet<MoodEntry> MoodEntries { get; set; }
    public DbSet<MoodSchedule> MoodSchedules { get; set;}
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
}
