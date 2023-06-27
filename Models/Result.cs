public class Result
{
    public string Region { get; }
    public List<Location> MatchedLocations { get; } = new List<Location>();

    public Result(string region)
    {
        Region = region;
    }
}



