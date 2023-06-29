public class Region
{
    public string Name { get; }
    public List<MyPolygon> Polygons { get; }

    public Region(string name, List<MyPolygon> coordinates)
    {
        Name = name;
        Polygons = coordinates;
    }
}
