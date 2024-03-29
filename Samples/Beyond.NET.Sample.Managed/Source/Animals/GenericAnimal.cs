namespace Beyond.NET.Sample;

public class GenericAnimal: BaseAnimal
{
    public override string Name { get; }

    public GenericAnimal(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}