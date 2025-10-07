namespace Beyond.NET.Sample;

public struct Point
{
    public double X { get; }
    public double Y { get; }

    public Point(
        double x,
        double y
    )
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{{ X: {X}; Y: {Y} }}";
    }
}

public enum StepMode
{
    Over,
    In,
    Out,
}

// TODO: Delegates with ref, out, in parameters
public static class DelegatesTest
{
    public delegate int TransformIntDelegate(int i);
    public delegate int TransformIntWithRefDelegate(ref int iRef);
    public delegate void TransformIntWithOutDelegate(int i, out int iOut);
    public delegate StepMode DebugEventHandler(object sender, StepMode stepMode);

    public static int TransformInt(
        int i,
        TransformIntDelegate intTransformer
    )
    {
        int result = intTransformer(i);

        return result;
    }

    public static int TransformIntWithRef(
        ref int iRef,
        TransformIntWithRefDelegate intTransformer
    )
    {
        int result = intTransformer(ref iRef);

        return result;
    }

    public static void TransformIntWithOut(
        int i,
        TransformIntWithOutDelegate intTransformer
    )
    {
        intTransformer(i, out i);
    }

    public delegate Point PointTransformDelegate(Point point);
    public delegate Point PointTransformWithRefDelegate(ref Point pointRef);

    public static Point TransformPoint(
        Point point,
        PointTransformDelegate pointTransformer
    )
    {
        Point result = pointTransformer(point);

        return result;
    }

    public static Point TransformRefPoint(
        ref Point pointRef,
        PointTransformWithRefDelegate pointTransformer
    )
    {
        Point result = pointTransformer(ref pointRef);

        return result;
    }

    public static StepMode TransformStepMode(StepMode mode)
    {
        return mode;
    }
}
