using Microsoft.Extensions.DependencyInjection;

namespace FuelPriceService
{
    internal class Program
    {
        private static FuelPriceContext fuelPriceContext;

        public static void Main()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("blah-blah"));

            var serviceProvider = services.BuildServiceProvider();
        }
    }
}


