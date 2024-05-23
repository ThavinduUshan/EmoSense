namespace api;

public interface IMoodScheduleService
{
    Task<List<MoodScheduleResponseDto>> GetSchedules();
    Task<MoodScheduleResponseDto> GetScheduleById(int id);
    Task<List<MoodScheduleResponseDto>> AddSchedule(MoodScheduleRequestDto dto);
    Task<List<MoodScheduleResponseDto>> UpdateSchedule(int id, MoodScheduleRequestDto dto);
    Task<List<MoodScheduleResponseDto>> DeleteSchedule(int id);
}
