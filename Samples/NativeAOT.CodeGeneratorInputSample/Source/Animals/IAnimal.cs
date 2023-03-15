namespace NativeAOT.CodeGeneratorInputSample;

public interface IAnimal
{
    string Name { get; }

    string Eat(string food);
}