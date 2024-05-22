using System.ComponentModel.DataAnnotations;

namespace api;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public DateTime AddedAt {get; set; } = DateTime.Now;

    public ICollection<MoodEntry> MoodEntries { get; set; }
    public ICollection<MoodSchedule> MoodSchedules { get; set; }
    public int UserRoleId { get; set; } 
    public UserRole UserRole{ get; set; } 
}
