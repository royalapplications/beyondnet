namespace NativeAOT.CodeGeneratorInputSample;

public abstract class BaseAnimal: IAnimal
{
    public abstract string Name { get; }

    public virtual void Eat(string food)
    {
        Console.WriteLine($"{Name} is eating {food}.");
    }
}