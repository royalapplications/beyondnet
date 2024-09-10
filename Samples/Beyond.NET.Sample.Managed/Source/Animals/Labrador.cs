namespace Beyond.NET.Sample;

public class Labrador: Dog
{
    public new const string StaticName = "Labrador";
    public const string LabradorName = StaticName;
    
    public override string Name => LabradorName;
    
    public Labrador() { }
}