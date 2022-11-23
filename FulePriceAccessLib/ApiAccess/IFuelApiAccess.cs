using FuelPriceAccessLib.Models;

namespace FuelPriceAccessLib.ApiAccess
{
    public interface IFuelApiAccess
    {
        FuelPrice FuelPrice { get; set; }

        FuelPrice GetApiData(int dayParameter);
    }
}