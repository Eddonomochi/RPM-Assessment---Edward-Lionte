using FuelPricingService.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuelPricingService
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager objsync = new DataManager();
            objsync.GetFuelApiData().Wait();
        }
    }
}