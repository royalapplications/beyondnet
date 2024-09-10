namespace Beyond.NET.Sample;

public class Dog: BaseAnimal
{
    public const string StaticName = "Dog";
    public const string DogName = StaticName;
    
    public override string Name => DogName;
    
    internal Dog() { }
}