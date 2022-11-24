using FuelPricingService.Engine;
using FuelPricingService.Manager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FuelPricingService;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .UseWindowsService(options => { options.ServiceName = "Fuel Price DB Service"; })
            .ConfigureServices(services =>
            {
                services.AddSingleton<DataEngine>();
                services.AddSingleton<Startup>();
                services.AddSingleton<FuelPriceDBManager>();
                services.AddHostedService<SchedulerService>();
            })
            .Build();

        await host.RunAsync();
    }
}