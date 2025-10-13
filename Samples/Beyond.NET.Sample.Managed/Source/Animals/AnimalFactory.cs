namespace Beyond.NET.Sample;

public delegate IAnimal? AnimalCreatorDelegate(string animalName);

public static class AnimalFactory
{
    public static readonly AnimalCreatorDelegate DEFAULT_CREATOR = (animalName) =>
    {
        return animalName switch {
            Dog.DogName => new Dog(),
            Cat.CatName => new Cat(),
            _ => null
        };
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

    public static T CreateAnimal<T>() where T: class, IAnimal
    {
        if (typeof(T).IsAssignableTo(typeof(Cat))) {
            return (T)(IAnimal)new Cat();
        } else if (typeof(T).IsAssignableTo(typeof(Dog))) {
            return (T)(IAnimal)new Dog();
        }

        throw new Exception("Unknown Animal Type");
    }
}
