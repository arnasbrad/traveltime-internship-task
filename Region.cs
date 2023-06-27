public class Region
{
    public string Name { get; }
    public List<List<List<double>>> Coordinates { get; }

    public Region(string name, List<List<List<double>>> coordinates)
    {
        Name = name;
        Coordinates = coordinates;
    }
}



