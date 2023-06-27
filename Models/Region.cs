using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Region
{
    public string Name { get; }
    public List<List<MyCoordinate>> Coordinates { get; }

    public Region(string name, List<List<MyCoordinate>> coordinates)
    {
        Name = name;
        Coordinates = coordinates;
    }
}

[JsonConverter(typeof(CoordinateConverter))]
public class MyCoordinate
{
    public double X { get; set; }
    public double Y { get; set; }
}

public class CoordinateConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(MyCoordinate));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JArray array = JArray.Load(reader);

        return new MyCoordinate
        {
            X = array[0].Value<double>(),
            Y = array[1].Value<double>()
        };
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}