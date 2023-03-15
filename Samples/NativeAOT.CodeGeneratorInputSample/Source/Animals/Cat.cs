namespace NativeAOT.CodeGeneratorInputSample;

public class Cat: IAnimal
{
    public const string CatName = "Cat";

    public string Name => CatName;
    
    internal Cat() { }
}