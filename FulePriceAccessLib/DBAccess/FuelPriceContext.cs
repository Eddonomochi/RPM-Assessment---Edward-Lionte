using Arch.EntityFrameworkCore;
using FuelPriceAccessLib.Models;

namespace FuelPriceAccessLib.DBAccess
{
    public class FuelPriceContext : DbContext
    {
        public FuelPriceContext(DbContextOptions options) : base(options) { }

        public DbSet<FuelPrice> FuelPrices { get; set; }

    }
}
