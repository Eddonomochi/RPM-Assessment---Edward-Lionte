using Newtonsoft.Json;
using System.Net.Http.Headers;
using FuelPricingService.DBContext;
using System.Text.RegularExpressions;

namespace FuelPricingService.Manager
{
    public class DataManager : IDataManager
    {

        public async Task GetFuelApiData()
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri("http://api.eia.gov/series/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(
                "data/?api_key=ec92aacd6947350dcb894062a4ad2d08&series_id=PET.EMD_EPD2D_PTE_NUS_DPG.W&api_key=ec92aacd6947350dcb894062a4ad2d08");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var rootObj = JsonConvert.DeserializeObject<Root>(jsonData);

                foreach (var repo in rootObj.series_data)
                {
                    int counter = 0;
                    foreach (var data in repo.data)
                    {
                        FuelPriceContext priceContext = new FuelPriceContext();
                        
                        foreach (var dataPoint in data)
                        {
                            if (dataPoint.ToString().Length == 8 && !dataPoint.ToString().Contains('.'))
                            {
                                priceContext.FuelPriceDate = dataPoint.ToString();
                            }
                            else
                            {
                                priceContext.FuelPrice = dataPoint.ToString();
                            }
                        }
                        counter++;

                        Console.WriteLine($"Number: {counter} Date: {priceContext.FuelPriceDate} Price: {priceContext.FuelPrice}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}