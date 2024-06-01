
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace api;

public class MoodScheduleService : IMoodScheduleService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemoryCache _cache;

    public MoodScheduleService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _cache = cache;
    }
    public async Task<List<MoodScheduleResponseDto>> AddSchedule(MoodScheduleRequestDto dto)
    {
        var schedule = _mapper.Map<MoodSchedule>(dto);
        schedule.UserId = GetUserId();
        await _context.MoodSchedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
        
        //set cache for background service with all schedules.
        var scheduleList = await _context.MoodSchedules.Where(s => s.ScheduledAt > DateTime.Now).ToListAsync();
        _cache.Set("Futureschedules", scheduleList);

        return await GetSchedules();
    }

    public async Task<List<MoodScheduleResponseDto>> DeleteSchedule(int id)
    {
        var schedule = await _context.MoodSchedules.FirstOrDefaultAsync(s => s.Id == id);
        _context.MoodSchedules.Remove(schedule);
        await _context.SaveChangesAsync();
        
        return await GetSchedules();
    }

    public async Task<MoodScheduleResponseDto> GetScheduleById(int id)
    {
        var schedule = await _context.MoodSchedules.FirstOrDefaultAsync(s => s.Id == id);
        var response = _mapper.Map<MoodScheduleResponseDto>(schedule);
        return response;
    }

    public async Task<List<MoodScheduleResponseDto>> GetSchedules()
    {
        var userId = GetUserId();
        var schedules = await _context.MoodSchedules.Where(s => s.UserId == userId).ToListAsync();
        var response = _mapper.Map<List<MoodScheduleResponseDto>>(schedules);
        return response;
    }

    public async Task<List<MoodScheduleResponseDto>> UpdateSchedule(int id, MoodScheduleRequestDto dto)
    {
        var schedule = await _context.MoodSchedules.FirstOrDefaultAsync(s => s.Id == id);

        //request method must be PATCH
        schedule.ScheduledAt = dto.ScheduledAt;

        await _context.SaveChangesAsync();

        return await GetSchedules();
    }

    private int GetUserId()
    {
        return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"));
    }
}
