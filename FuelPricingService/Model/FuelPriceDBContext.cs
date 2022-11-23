using Microsoft.EntityFrameworkCore;

namespace FuelPricingService.Model;

public class FuelPriceDBContext : DbContext
{
    public FuelPriceDBContext(string connectionString, int daysLookBack)
    {
        cnn = connectionString;
        dlb = daysLookBack;
    }

    private string cnn { get; }
    public int dlb { get; }
    public DbSet<FuelPrices> FuelPrices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(cnn);
    }
}