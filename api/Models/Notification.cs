namespace api;

public class Notification
{
    public int Id { get; set; }
    public bool IsSent { get; set; } = true;

    public int UserId { get; set; }
    public User User { get; set; }
}
