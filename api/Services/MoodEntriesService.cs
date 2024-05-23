using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api;

public class MoodEntriesService : IMoodEntriesService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MoodEntriesService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<List<MoodEntryResponseDto>> AddEntry(MoodEntryRequestDto dto)
    {
        var entry = _mapper.Map<MoodEntry>(dto);
        entry.UserId = GetUserId();
        await _context.MoodEntries.AddAsync(entry);
        await _context.SaveChangesAsync();

        return await GetEntries();
    }

    public async Task<List<MoodEntryResponseDto>> DeleteEntry(int id)
    {
        var entry = await _context.MoodEntries.FirstOrDefaultAsync(m => m.Id == id);
        _context.MoodEntries.Remove(entry);
        await _context.SaveChangesAsync();

        return await GetEntries();
    }

    public async Task<List<MoodEntryResponseDto>> GetEntries()
    {
        var userId = GetUserId();
        var newEntries = await _context.MoodEntries.Where(m => m.UserId == userId).ToListAsync();
        var entriesResponse = _mapper.Map<List<MoodEntryResponseDto>>(newEntries);
        return entriesResponse;
    }

    private int GetUserId()
    {
        return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"));
    }

    public async Task<MoodEntryResponseDto> GetEntryById(int id)
    {
        var entry  = await _context.MoodEntries.FirstOrDefaultAsync(m => m.Id == id);
        var entryResponse = _mapper.Map<MoodEntryResponseDto>(entry);
        return entryResponse;
    }

    public async Task<List<MoodEntryResponseDto>> UpdateEntry(int id, MoodEntryRequestDto dto)
    {
        var entry  = await _context.MoodEntries.FirstOrDefaultAsync(m => m.Id == id);
        
        //the request method must be PATCH
        entry.Note = dto.Note;
        entry.Score = dto.Score;

        await _context.SaveChangesAsync();

        return await GetEntries();
    }
}
