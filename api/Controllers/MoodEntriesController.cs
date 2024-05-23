using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api;

[ApiController]
[Route("api/moods"), Authorize(Roles = "User")]
public class MoodEntriesController : ControllerBase
{
    private readonly IMoodEntriesService _moodEntriesService;

    public MoodEntriesController(IMoodEntriesService moodEntriesService)
    {
        _moodEntriesService = moodEntriesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MoodEntryResponseDto>>> GetAll()
    {
        var response = await _moodEntriesService.GetEntries();
        return Ok(response);
    }

    [HttpGet("id")]
    public async Task<ActionResult<MoodEntryResponseDto>> GetById(int id)
    {
        var response = await _moodEntriesService.GetEntryById(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<List<MoodEntryResponseDto>>> Add(MoodEntryRequestDto dto)
    {
        var response = await _moodEntriesService.AddEntry(dto);
        return Ok(response);    
    }

    [HttpPatch("id")]
    public async Task<ActionResult<List<MoodEntryResponseDto>>> Update(int id, MoodEntryRequestDto dto)
    {
        var response = await _moodEntriesService.UpdateEntry(id, dto);
        return Ok(response);
    }
    [HttpDelete("id")]
    public async Task<ActionResult<List<MoodEntryResponseDto>>> Delete(int id)
    {
        var response = await _moodEntriesService.DeleteEntry(id);
        return Ok(response); 
    }
}
