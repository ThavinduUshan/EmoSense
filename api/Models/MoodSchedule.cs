using System.ComponentModel.DataAnnotations;

namespace api;

public class MoodSchedule
{
    public int Id { get; set; }
    [Required]
    public DateTime ScheduledAt { get; set; }

}
