using Microsoft.EntityFrameworkCore;

namespace FuelPricingService.Model;

public class FuelPriceDBContext : DbContext
{
    public FuelPriceDBContext(string connectionString)
    {
        cnn = connectionString;
    }
    public  DbSet<FuelPrices> FuelPrices { get; set; }
    
    private string cnn { get; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(cnn);
    }
}