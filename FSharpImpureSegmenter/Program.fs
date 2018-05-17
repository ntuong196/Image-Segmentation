module Program

[<EntryPoint>]
let main argv =
    // load a Tiff image
    let image = TiffModule.loadImage argv.[0]
    // Alternative Code:
    // let image = TiffModule.loadImage "D://QUTLecture//2018s1//CAB402//Assignment1//CAB401-Report-Asignment1//SegmentationSkeleton//TestImages//L15-3792E-1717N-Q4.tif"

    // testing using sub-image of size 32x32 pixels
    // let N =5, execution time: 5 mins 11 secs
    // let N =4, execution time: 14 secs
    let N = 5
   
    // increasing this threshold will result in more segment merging and therefore fewer final segments
    let threshold = 800.0

    // determine the segmentation for the (top left corner of the) image (2^N x 2^N) pixels
    let segmentation = SegmentationModule.segment image N threshold

    // draw the (top left corner of the) original image but with the segment boundaries overlayed in blue
    TiffModule.overlaySegmentation image "impureSegmented.tif" N segmentation

    0 // return an integer exit code