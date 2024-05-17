using Microsoft.EntityFrameworkCore;

namespace api;

public class DataContext : DbContext
{
    public DbSet<MoodEntry> MoodEntries { get; set; }
    public DbSet<MoodSchedule> MoodSchedules { get; set;}
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
}
