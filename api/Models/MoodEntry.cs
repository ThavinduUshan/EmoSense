using System.ComponentModel.DataAnnotations;

namespace api;

public class MoodEntry
{
    public int Id { get; set; }
    [Required]
    public float Score { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime AddedAt { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public User User{ get; set; }

}
