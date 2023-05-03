namespace Beyond.NET.Sample;

public class Cat: BaseAnimal
{
    public const string CatName = "Cat";

    public override string Name => CatName;

    internal Cat() { }
}