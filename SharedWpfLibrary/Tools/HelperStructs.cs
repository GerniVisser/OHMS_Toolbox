using System.Collections.Generic;

namespace SharedWpfLibrary.Tools
{
    public class Coord
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    public class CoordSerries
    {
        public CoordSerries()
        {
            coords = new List<Coord>();
        }

        public List<Coord> coords { get; set; }
    }
}
