namespace NativeAOT.CodeGeneratorInputSample;

public delegate Address MoveDelegate(string newStreet, string newCity);

public class Address
{
    public string Street { get; }
    public string City { get; }

    public Address(string street, string city)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
    }

    public Address Move(MoveDelegate mover, string newStreet, string newCity)
    {
        var newAddress = mover(newStreet, newCity);

        return newAddress;
    }
}