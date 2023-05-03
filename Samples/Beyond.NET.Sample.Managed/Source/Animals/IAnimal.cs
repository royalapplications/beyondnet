namespace Beyond.NET.Sample;

public interface IAnimal
{
    string Name { get; }

    string Eat(string food);
}