using FuelPricingService.Manager;
using FuelPricingService.Model;

namespace FuelPricingService;

internal class Program
{
    private static void Main(string[] args)
    {
        var start = new Startup();
        var dataManager = new DataManager();
        var dataBaseObject = new FuelPriceDBContext(start.conn, start.DaysLookBack);

        dataManager.GetFuelApiData(dataBaseObject).Wait();
    }
}