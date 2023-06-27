public class Result
{
    public string Region { get; }
    public List<string> MatchedLocations { get; } = new List<string>();

    public Result(string region)
    {
        Region = region;
    }
}



