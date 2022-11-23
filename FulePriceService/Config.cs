using FuelPriceAccessLib.DBAccess;
using FuelPriceAccessor.DataAccess;
using Microsoft.Extensions.Configuration;

namespace FuelPriceService
{
    public class Config
    {
        private static FuelPriceContext fuelPriceContext;

        public Config(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    }
}