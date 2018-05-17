using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace CSharpSegmenter
{
    class Program
    {
        static void Main(string[] args)
        {
            // load a Tiff image
            // FIX HERE
            TiffImage image = new TiffImage("..//TestImages//L15-3792E-1717N-Q4.tif");

            // testing using sub-image of N 32x32 pixels
            // let N =5, execution time: 5 secs
            int N = 5;

            // increasing this threshold will result in more segment merging and therefore fewer final segments
            int threshold = 800;
            
            //passing the image, the N (2*N x 2*N image ) and the threshold into the segmentation module
            SegmentationModule segmentation = new SegmentationModule(image, N, threshold);
            //grow the Segmentation in the module from an emty dictionary            
            segmentation.GrowUntilNoChange();

            // draw the (top left corner of the) original image but with the segment boundaries overlayed in blue
            image.overlaySegmentation("csharpSegmented.tif", N, segmentation.Segment);
        }
    }
}

