namespace Beyond.NET.Sample;

public class Dog: BaseAnimal
{
    public const string DogName = "Dog";
    
    public override string Name => DogName;
    
    internal Dog() { }
}