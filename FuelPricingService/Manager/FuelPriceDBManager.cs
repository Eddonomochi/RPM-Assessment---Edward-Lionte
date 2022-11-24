using FuelPricingService.Engine;
using FuelPricingService.Model;

namespace FuelPricingService.Manager;

public class FuelPriceDBManager
{
    private readonly DataEngine dataEngine = new();
    private readonly Startup startup = new();

    public FuelPriceDBManager(DataEngine engine, Startup start)
    {
        dataEngine = engine;
        startup = start;
    }

    public void Run()
    {
        var fuelPriceDbObject = new FuelPriceDBContext(startup.conn, startup.DaysLookBack);

        dataEngine.GetFuelApiData(fuelPriceDbObject).Wait();
    }
}