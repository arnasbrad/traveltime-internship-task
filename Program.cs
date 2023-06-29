using Newtonsoft.Json;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine(
                "Usage: dotnet run <path_to_regions.json> <path_to_locations.json> <path_to_results.json>"
            );
            Environment.Exit(1);
        }

        var regions = TaskUtils.LoadData<Region>(args[0]);
        var locations = TaskUtils.LoadData<Location>(args[1]);

        if (regions == null || locations == null)
        {
            Console.WriteLine("There was an exception with the input files");
            Environment.Exit(1);
        }

        var results = TaskUtils.FindLocationsInRegions(regions, locations);
        File.WriteAllText(args[2], JsonConvert.SerializeObject(results, Formatting.Indented));
    }
}
