using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSegmenter
{
    // Set up getter and setter for Colour class
    public class Colour
    {
        public List<Byte> colours { get; set; }
        public Colour(List<byte> colours)
        {
            this.colours = colours;
        }
    }
}
