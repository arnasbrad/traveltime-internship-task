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

        File.WriteAllText(resultsFile, JsonConvert.SerializeObject(results, Formatting.Indented));
    }
}


