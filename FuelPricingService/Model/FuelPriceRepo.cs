namespace FuelPricingService.Model;

public class Request
{
    public string command { get; set; }
    public string series_id { get; set; }
}

public class SeriesData
{
    public string series_id { get; set; }
    public IList<IList<object>> data { get; set; }
}

public class Root
{
    public Request request { get; set; }
    public IList<SeriesData> series_data { get; set; }
}