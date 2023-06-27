using Newtonsoft.Json;

public class Result
{
    public string Region { get; }

    [JsonIgnore]
    public List<Location> MatchedLocations { get; } = new List<Location>();

    public List<string> MatchedLocationNames 
    { 
        get { return MatchedLocations.Select(loc => loc.Name).ToList(); } 
    }

    public Result(string region)
    {
        Region = region;
    }
}
