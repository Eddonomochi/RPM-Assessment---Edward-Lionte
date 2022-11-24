using System.ComponentModel.DataAnnotations;

namespace FuelPricingService.Model;

public class FuelPrices
{
    public DateTime InsertDate { get; set; }

    [Key] public DateTime FuelPriceDate { get; set; }

    public decimal FuelPrice { get; set; }
}