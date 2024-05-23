namespace api;

public class MoodEntryRequestDto
{
    public float Score { get; set; }
    public string Note { get; set; } = string.Empty;
}
