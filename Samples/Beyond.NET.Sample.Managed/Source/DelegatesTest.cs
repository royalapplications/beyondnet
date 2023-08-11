// namespace Beyond.NET.Sample.Source;
//
// public struct Point
// {
//     public double X { get; }
//     public double Y { get; }
//
//     public Point(
//         double x,
//         double y
//     )
//     {
//         X = x;
//         Y = y;
//     }
//
//     public override string ToString()
//     {
//         return $"{{ X: {X}; Y: {Y} }}";
//     }
// }
//
// public static class DelegatesTest
// {
//     public delegate Point PointTransformWithInDelegate(in Point inPoint);
//     public delegate Point PointTransformDelegate(Point inPoint);
//
//     public static Point TransformInPoint(
//         in Point inPoint,
//         PointTransformWithInDelegate pointTransformer
//     )
//     {
//         Point outPoint = pointTransformer(inPoint);
//
//         return outPoint;
//     }
//     
//     public static Point TransformPoint(
//         Point point,
//         PointTransformDelegate pointTransformer
//     )
//     {
//         Point outPoint = pointTransformer(point);
//
//         return outPoint;
//     }
//
//     public static void TestMultiplyPointByItself(Point inP)
//     {
//         Point outP = TransformInPoint(inP, (in Point innerInP) => {
//             Point innerOutP = new(
//                 innerInP.X * innerInP.X, 
//                 innerInP.Y * innerInP.Y
//             );
//
//             return innerOutP;
//         });
//         
//         Console.WriteLine($"In: {inP}; Out: {outP}");
//     }
// }