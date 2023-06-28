using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Region
{
    public string Name { get; }
    public List<List<MyCoordinate>> Coordinates { get; }

    public Region(string name, List<List<MyCoordinate>> coordinates)
    {
        Name = name;
        Coordinates = coordinates;
    }
}
