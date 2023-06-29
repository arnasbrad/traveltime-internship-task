using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PolygonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(MyPolygon);
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer
    )
    {
        JArray array = JArray.Load(reader);
        var coordinates = new List<MyCoordinate>();

        foreach (var item in array)
        {
            if (item is JArray coordinateArray && coordinateArray.Count >= 2)
            {
                MyCoordinate coordinate = new MyCoordinate
                {
                    X = coordinateArray[0]?.Value<double>() ?? default,
                    Y = coordinateArray[1]?.Value<double>() ?? default
                };
                coordinates.Add(coordinate);
            }
        }

        return new MyPolygon(coordinates);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
