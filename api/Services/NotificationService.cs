using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api;

public class NotificationService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;
    private readonly ILogger<NotificationService> _logger;
    private Timer _timer;

    public NotificationService(IServiceScopeFactory scopeFactory, IMemoryCache cache, IMapper mapper, ILogger<NotificationService> logger)
    {
        _scopeFactory = scopeFactory;
        _cache = cache;
        _mapper = mapper;
        _logger = logger;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await PopulateCache();
        _timer = new Timer(CheckSchedules, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
    }

    private async void CheckSchedules(object state)
    {
        using var scope = _scopeFactory.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var schedules = _cache.Get<List<MoodSchedule>>("Futureschedules");
        var matchingSchedules = schedules.Where(s => s.ScheduledAt.TruncateToMinute() == DateTime.Now.TruncateToMinute()).ToList();
        _logger.LogInformation("No of scheudules {schedules} at {time}", matchingSchedules.Count, DateTime.Now.TruncateToMinute());
        if (matchingSchedules.Count > 0)
        {
            var notificationsList = _mapper.Map<List<Notification>>(matchingSchedules);
            await _context.Notifications.AddRangeAsync(notificationsList);
            await _context.SaveChangesAsync();
        }

        _logger.LogInformation("CheckSchedules method executed at: {time}", DateTime.Now);
    }

    private async Task PopulateCache()
    {
        using var scope = _scopeFactory.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var schedules = await _context.MoodSchedules.Where(s => s.ScheduledAt > DateTime.Now).ToListAsync();
        _cache.Set("Futureschedules", schedules);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}
