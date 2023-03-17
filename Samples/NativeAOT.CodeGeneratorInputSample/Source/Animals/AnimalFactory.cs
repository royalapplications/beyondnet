namespace NativeAOT.CodeGeneratorInputSample;

public delegate IAnimal? AnimalCreatorDelegate(string animalName);

public static class AnimalFactory
{
    public static readonly AnimalCreatorDelegate DEFAULT_CREATOR = (animalName) => {
        switch (animalName) {
            case Dog.DogName:
                return new Dog();
            case Cat.CatName:
                return new Cat();
        }

        return null;
    };
    
    public static IAnimal? CreateAnimal(string animalName)
    {
        return CreateAnimal(
            animalName,
            DEFAULT_CREATOR
        );
    }
    
    public static IAnimal? CreateAnimal(
        string animalName,
        AnimalCreatorDelegate creator
    )
    {
        return creator(animalName);
    }
}