using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FuelPricingService;

internal class Startup
{
    private static readonly IConfigurationBuilder builder =
        new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

    public static IConfigurationRoot configuration = builder.Build();

    public string conn = configuration.GetConnectionString("DBConnection");

}