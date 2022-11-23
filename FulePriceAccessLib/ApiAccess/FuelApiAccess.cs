using FuelPriceAccessLib.Models;

namespace FuelPriceAccessLib.ApiAccess
{
    public class FuelApiAccess : IFuelApiAccess
    {
        public FuelApiAccess()
        {

        }

        public FuelPrice FuelPrice { get; set; }

        public FuelPrice GetApiData(int dayParameter)
        {
            return FuelPrice;
        }
    }
}