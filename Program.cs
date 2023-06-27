using NetTopologySuite.Geometries;
using Newtonsoft.Json;

class Program
{
    public static void Main(string[] args)
    {
        string regionsFile = args[0];
        string locationsFile = args[1];
        string resultsFile = args[2];

        bool isRegionsFileValid = TaskUtils.IsJsonFileValid(regionsFile);
        bool isLocationsFileValid = TaskUtils.IsJsonFileValid(locationsFile);

        if(!isLocationsFileValid || !isRegionsFileValid) return;

        var regions = TaskUtils.DeserializeJson<Region>(File.ReadAllText(regionsFile));
        var locations = TaskUtils.DeserializeJson<Location>(File.ReadAllText(locationsFile));

        if(regions == null || locations == null)
        {
            System.Console.WriteLine("There was an exception with the input files");
            return;
        }

        var results = TaskUtils.FindLocationsInRegions(regions, locations); 

        File.WriteAllText(resultsFile, JsonConvert.SerializeObject(results, Formatting.Indented));
    }
}


