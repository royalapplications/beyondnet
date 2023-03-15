namespace NativeAOT.CodeGeneratorInputSample;

public static class AnimalFactory
{
    public static IAnimal? CreateAnimal(string animalName)
    {
        switch (animalName) {
            case Dog.DogName:
                return new Dog();
            case Cat.CatName:
                return new Cat();
        }

        return null;
    }
}