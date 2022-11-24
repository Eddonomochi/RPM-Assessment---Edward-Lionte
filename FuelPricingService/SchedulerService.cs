using FuelPricingService.Manager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FuelPricingService;

public class SchedulerService : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
    private readonly IServiceScopeFactory factory;

    private readonly ILogger<SchedulerService> logger;
    private int executionCount;

    public SchedulerService(ILogger<SchedulerService> iLogger, IServiceScopeFactory scopeFactory)
    {
        logger = iLogger;
        factory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //timer = new Timer(Run, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

        using var timer = new PeriodicTimer(_period);
        while (
            !stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
            try
            {
                await using var asyncScope = factory.CreateAsyncScope();
                var manager = asyncScope.ServiceProvider.GetRequiredService<FuelPriceDBManager>();
                manager.Run();

                executionCount++;
                logger.LogInformation(
                    $"Executed Fuel Price DB Migration - Count: {executionCount}");
            }
            catch (Exception ex)
            {
                logger.LogInformation(
                    $"Failed to execute Fuel Price DB Migration with exception message: {ex.Message}");
            }
    }
}