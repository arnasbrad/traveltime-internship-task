using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

static class TaskUtils
{
    public static bool IsJsonFileValid(string filePath)
    {
        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("The file {0} does not exist.", filePath);
            return false;
        }

        // Check if the file is not empty
        if (new FileInfo(filePath).Length == 0)
        {
            Console.WriteLine("The file {0] is empty.", filePath);
            return false;
        }

        // Read the file content
        string content = File.ReadAllText(filePath);

        // Check if the content is valid JSON
        if (!IsValidJson(content))
        {
            Console.WriteLine("The file content of {0} is not valid JSON.", filePath);
            return false;
        }

        return true;
    }

    public static List<T>? DeserializeJson<T>(string content)
    {
        try
        {
            // Try to deserialize the content
            var items = JsonConvert.DeserializeObject<List<T>>(content);
            return items;
        }
        catch (JsonSerializationException ex)
        {
            // If the JSON structure does not match the class structure,
            // Json.NET will throw a JsonSerializationException
            Console.WriteLine(
                $"JSON structure does not match the {typeof(T).Name} class structure: " + ex.Message
            );
            return null;
        }
    }

    public static bool IsValidJson(string strInput)
    {
        strInput = strInput.Trim();
        if (
            (strInput.StartsWith("{") && strInput.EndsWith("}"))
            || //For object
            (strInput.StartsWith("[") && strInput.EndsWith("]"))
        ) //For array
        {
            try
            {
                var obj = JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException jex)
            {
                //Exception in parsing json
                Console.WriteLine("Exception in parsing json: " + jex.Message);
                return false;
            }
            catch (Exception ex) //some other exception
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static List<Result>? FindLocationsInRegions(
        List<Region> regions,
        List<Location> locations
    )
    {
        var results = new List<Result>();
        var geometryFactory = new GeometryFactory();

        foreach (var region in regions)
        {
            var polygons = new List<Polygon>();
            foreach (var polygonCoords in region.Polygons)
            {
                var coordinates = new List<Coordinate>();
                foreach (var coord in polygonCoords)
                {
                    coordinates.Add(new Coordinate(coord.X, coord.Y));
                }

                polygons.Add(geometryFactory.CreatePolygon(coordinates.ToArray()));
            }

            var multiPolygon = geometryFactory.CreateMultiPolygon(polygons.ToArray());

            var result = new Result(region.Name);

            foreach (var location in locations)
            {
                var point = geometryFactory.CreatePoint(
                    new Coordinate(location.Coordinates[0], location.Coordinates[1])
                );
                if (multiPolygon.Covers(point))
                {
                    result.MatchedLocations.Add(location);
                }
            }

            results.Add(result);
        }

        return results;
    }
}
