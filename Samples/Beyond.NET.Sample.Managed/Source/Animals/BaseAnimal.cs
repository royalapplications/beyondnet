namespace Beyond.NET.Sample;

public abstract class BaseAnimal: IAnimal
{
    public abstract string Name { get; }

    public virtual string Eat(string food)
    {
        return $"{Name} is eating {food}.";
    }
}