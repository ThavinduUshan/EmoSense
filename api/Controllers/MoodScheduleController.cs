using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api;

[ApiController]
[Route("api/schedules"), Authorize(Roles = "User")]
public class MoodScheduleController : ControllerBase
{
    private readonly IMoodScheduleService _moodScheduleService;

    public MoodScheduleController(IMoodScheduleService moodScheduleService)
    {
        _moodScheduleService = moodScheduleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MoodScheduleResponseDto>>> GetAll()
    {
        var response = await _moodScheduleService.GetSchedules();
        return Ok(response);
    }

    [HttpGet("id")]
    public async Task<ActionResult<MoodScheduleResponseDto>> GetById(int id)
    {
        var response = await _moodScheduleService.GetScheduleById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<MoodScheduleResponseDto>> Add(MoodScheduleRequestDto dto)
    {
        var response = await _moodScheduleService.AddSchedule(dto);
        return Ok(response);
    }
    
    [HttpPatch("id")]
    public async Task<ActionResult<List<MoodScheduleResponseDto>>> Update(int id, MoodScheduleRequestDto dto)
    {
        var response = await _moodScheduleService.UpdateSchedule(id, dto);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<List<MoodScheduleResponseDto>>> Delete(int id)
    {
        var response = await _moodScheduleService.DeleteSchedule(id);
        return Ok(response);
    }

}
