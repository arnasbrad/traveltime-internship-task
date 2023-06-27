using Newtonsoft.Json;

public class Location
{
    public string Name { get; }

    [JsonIgnore]
    public List<double> Coordinates { get; }

    public Location(string name, List<double> coordinates)
    {
        Name = name;
        Coordinates = coordinates;
    }
}
