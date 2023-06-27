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
}

class Program
{
    public static void Main(string[] args)
    {
        var regions = JsonConvert.DeserializeObject<List<Region>>(File.ReadAllText("regions.json"));
        var locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText("locations.json"));

        var results = new List<Result>();
        var geometryFactory = new GeometryFactory();

        foreach (var region in regions)
        {
            var polygons = new List<Polygon>();
            foreach (var polygonCoords in region.Coordinates)
            {
                var coordinates = new List<Coordinate>();
                foreach (var coord in polygonCoords)
                {
                    coordinates.Add(new Coordinate(coord[0], coord[1]));
                }

                polygons.Add(geometryFactory.CreatePolygon(coordinates.ToArray()));
            }

            var multiPolygon = geometryFactory.CreateMultiPolygon(polygons.ToArray());

            var result = new Result(region.Name);

            foreach (var location in locations)
            {
                var point = geometryFactory.CreatePoint(new Coordinate(location.Coordinates[0], location.Coordinates[1]));
                if (multiPolygon.Covers(point))
                {
                    result.MatchedLocations.Add(location.Name);
                }
            }

            results.Add(result);
        }

        File.WriteAllText("results.json", JsonConvert.SerializeObject(results, Formatting.Indented));
    }
}


