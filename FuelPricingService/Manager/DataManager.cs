using System.Globalization;
using FuelPricingService.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FuelPricingService.Manager;

public class DataManager : IDataManager
{
    public async Task GetFuelApiData(FuelPriceDBContext fuelContext)
    {
        using HttpClient client = new();
        client.BaseAddress = new Uri("http://api.eia.gov/series/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await client.GetAsync(
            "data/?api_key=ec92aacd6947350dcb894062a4ad2d08&series_id=PET.EMD_EPD2D_PTE_NUS_DPG.W&api_key=ec92aacd6947350dcb894062a4ad2d08");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var rootObj = JsonConvert.DeserializeObject<Root>(jsonData);

            foreach (var repo in rootObj.series_data)
            {
                foreach (var data in repo.data)
                {
                    var fuelPrices = new FuelPrices();

                    foreach (var dataPoint in data)
                        if (dataPoint.ToString().Length == 8 && !dataPoint.ToString().Contains('.'))
                        {
                            var result = DateTime.ParseExact(dataPoint.ToString(), "yyyyMMdd",
                                CultureInfo.InvariantCulture);

                            fuelPrices.FuelPriceDate = result;
                        }
                        else
                            fuelPrices.FuelPrice = decimal.Parse(dataPoint.ToString());
                    InsertData(fuelContext, fuelPrices);
                }
            }
        }
    }


    private void InsertData(FuelPriceDBContext fuelContext, FuelPrices fuelPrices)
    {
        if (fuelPrices.FuelPriceDate >= DateTime.Today.AddDays(-fuelContext.dlb))
        {
            var foundFuelPrice = from b in fuelContext.FuelPrices
                where b.FuelPriceDate == fuelPrices.FuelPriceDate
                select b;

            if (foundFuelPrice.Count() == 0)
            {
                fuelPrices.InsertDate = DateTime.Now;
                fuelContext.Add(fuelPrices);
                fuelContext.SaveChanges();
            }
        }
    }

}