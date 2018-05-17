using System;

namespace CSharpSegmenter
{
    // Set up getter and setter for Coordinate class
    public class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
