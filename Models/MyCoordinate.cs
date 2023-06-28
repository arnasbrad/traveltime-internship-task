using Newtonsoft.Json;

[JsonConverter(typeof(CoordinateConverter))]
public class MyCoordinate
{
    public double X { get; set; }
    public double Y { get; set; }
}

