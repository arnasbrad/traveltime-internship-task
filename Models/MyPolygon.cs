using System.Collections;
using Newtonsoft.Json;

[JsonConverter(typeof(PolygonConverter))]
public class MyPolygon : IEnumerable<MyCoordinate>
{
    public List<MyCoordinate> Coordinates { get; set; }

    public MyPolygon(List<MyCoordinate> coordinates)
    {
        Coordinates = coordinates;
    }

    public IEnumerator<MyCoordinate> GetEnumerator()
    {
        return Coordinates.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
