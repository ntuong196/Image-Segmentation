module SegmentModule

type Coordinate = (int * int) // x, y coordinate of a pixel
type Colour = byte list       // one entry for each colour band, typically: [red, green and blue]

type Segment = 
    | Pixel of Coordinate * Colour
    | Parent of Segment * Segment 

// Helper function to calculate square of a number
let square x = x * x

// Convert segment to colour list
// If the segment is a Pixel, return the colour value as a list
// If the segment is a Parent of two segments, recursively apply the function for "children" segments
let rec convertSegment (segment: Segment) = 
    match segment with
    | Pixel(_,colour) -> [colour]
    | Parent(segment1, segment2) -> (convertSegment segment1) @ (convertSegment segment2)

// Transpose the list of pixel colours to the list of each colour band
let transpose (rows:list<list<'T>>) : list<list<'T>> =
    let n = List.length (List.head rows)
    List.init n (fun i -> (List.map (List.item i) rows))

// Convert colour byte list to float list
let rec convertByteToFloat list =
    match list with 
    | [] -> []
    | head::tail -> [List.map (fun x -> float x) head] @ (convertByteToFloat tail)

// return the list of standard deviations variance of the pixel colours
let stddevList list =
    let mean = list |> List.average
    let variance = list |> List.averageBy (fun x -> square(x - mean))
    sqrt(variance)

// find number of pixel
let rec numberOfPixel segment =
    match segment with
    | Pixel(_,_) -> 1.0
    | Parent(segment1, segment2) -> (numberOfPixel segment1) + (numberOfPixel segment2)

// return a list of the standard deviations of the pixel colours in the given segment
// the list contains one entry for each colour band, typically: [red, green and blue]
let stddev (segment: Segment) : float list =
    segment
    |> convertSegment 
    |> transpose
    |> convertByteToFloat 
    |> List.map stddevList 
    

// determine the cost of merging the given segments: 
// equal to the standard deviation of the combined the segments minus the sum of the standard deviations of the individual segments, 
// weighted by their respective sizes and summed over all colour bands
let mergeCost segment1 segment2 : float =
    let combine = Parent(segment1, segment2)
    // sum of the standard deviations of the individual segments
    let combineSum = combine |> stddev |>List.sum 
    let stddevSumSegment1 = segment1 |> stddev |>List.sum
    let stddevSumSegment2 = segment2 |> stddev |>List.sum 
    // determine the number of pixecl of the individual segments
    let numberPixel = numberOfPixel combine
    let numberPixel1 = numberOfPixel segment1
    let numberPixel2 = numberOfPixel segment2
    // the cost of merging the two given segments
    let cost = combineSum*numberPixel - stddevSumSegment1*numberPixel1 - stddevSumSegment2*numberPixel2
    cost