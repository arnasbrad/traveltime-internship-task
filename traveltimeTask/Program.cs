using NetTopologySuite.Geometries;
using Newtonsoft.Json;

public class Location
{
    public string Name { get; set; }
    public List<double> Coordinates { get; set; }

    public Location(string name, List<double> coordinates)
    {
        Name = name;
        Coordinates = coordinates;
    }
}

public class Region
{
    public string Name { get; set; }
    public List<List<List<double>>> Coordinates { get; set; }

    public Region(string name, List<List<List<double>>> coordinates)
    {
        Name = name;
        Coordinates = coordinates;
    }
}

public class Result
{
    public string Region { get; set; }
    public List<string> MatchedLocations { get; set; } = new List<string>();

    public Result(string region)
    {
        Region = region;
    }

    public Result(string region, List<string> matchedLocations)
    {
        Region = region;
        MatchedLocations = matchedLocations;
    }
}

class Program
{
    static void Main(string[] args)
    {
        //declaring IO files
        string regionsFile = "";
        string locationsFile = "";
        string resultsFile = "";
        if (args.Length == 3)
        {
            regionsFile = args[0].ToString();
            locationsFile = args[1].ToString();
            resultsFile = args[2].ToString();
        }
        else
        {
            System.Console.WriteLine(
                "Incorrect amount of variables passed (regions, locations, results)"
            );
            return;
        }

        //delete any results file that already exists
        if (File.Exists(resultsFile))
            File.Delete(resultsFile);

        //json file parsing
        List<Region>? regions = JsonConvert.DeserializeObject<List<Region>>(
            File.ReadAllText(regionsFile)
        );

        List<Location>? locations = JsonConvert.DeserializeObject<List<Location>>(
            File.ReadAllText(locationsFile)
        );

        //null checks
        if (regions == null)
        {
            System.Console.WriteLine("There was an error parsing the regions.json file");
            return;
        }

        if (locations == null)
        {
            System.Console.WriteLine("There was an error parsing the locations.json file");
            return;
        }

        List<Result>? results = new List<Result>();
        GeometryFactory geometryFactory = new GeometryFactory();

        foreach (Region region in regions)
        {
            List<Polygon> polygons = new List<Polygon>();
            foreach (List<List<double>> polygonCoords in region.Coordinates)
            {
                List<Coordinate> coordinates = new List<Coordinate>();
                foreach (List<double>? coord in polygonCoords)
                {
                    coordinates.Add(new Coordinate(coord[0], coord[1]));
                }

                polygons.Add(geometryFactory.CreatePolygon(coordinates.ToArray()));
            }

            MultiPolygon? multiPolygon = geometryFactory.CreateMultiPolygon(polygons.ToArray());

            Result? result = new Result(region.Name);

            foreach (Location location in locations)
            {
                Point? point = geometryFactory.CreatePoint(
                    new Coordinate(location.Coordinates[0], location.Coordinates[1])
                );
                if (multiPolygon.Covers(point))
                {
                    result.MatchedLocations.Add(location.Name);
                }
            }

            results.Add(result);
        }

        File.WriteAllText(resultsFile, JsonConvert.SerializeObject(results, Formatting.Indented));
    }
}
