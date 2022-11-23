using Newtonsoft.Json;
using System.Text.Json.Serialization;

//public sealed record class Repository(
//    [property: JsonPropertyName("series.0.data.o")] string FuelPriceDate)
//    //[property: JsonPropertyName("1")] string FuelPrice)
//{
//}
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



