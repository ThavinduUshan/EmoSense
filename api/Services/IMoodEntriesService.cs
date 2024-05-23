namespace api;

public interface IMoodEntriesService
{
    Task<List<MoodEntryResponseDto>> GetEntries();
    Task<MoodEntryResponseDto> GetEntryById(int id);
    Task<List<MoodEntryResponseDto>> AddEntry(MoodEntryRequestDto dto);
    Task<List<MoodEntryResponseDto>> UpdateEntry(int id, MoodEntryRequestDto dto);
    Task<List<MoodEntryResponseDto>> DeleteEntry(int id);
    
}
