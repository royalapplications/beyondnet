namespace NativeAOT.CodeGeneratorInputSample;

public class Dog: BaseAnimal
{
    public const string DogName = "Dog";
    
    public override string Name => DogName;
    
    internal Dog() { }
}