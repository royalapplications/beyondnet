namespace NativeAOT.CodeGeneratorInputSample;

public class Dog: IAnimal
{
    public const string DogName = "Dog";
    
    public string Name => DogName;
    
    internal Dog() { }
}