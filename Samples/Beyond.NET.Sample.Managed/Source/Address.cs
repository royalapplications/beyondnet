namespace Beyond.NET.Sample;

/// <summary>
/// A delegate that returns a new Address.
/// </summary>
public delegate Address MoveDelegate(string newStreet, string newCity);

/// <summary>
/// Describes an Address.
/// Summaries can have multiple lines.
/// Right?
/// </summary>
public class Address
{
    /// <summary>
    /// The Street of the Address.
    /// </summary>
    public string Street { get; }
    
    /// <summary>
    /// The City of the Address.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Constructs an Address.
    /// </summary>
    /// <param name="street">The Street of the Address.</param>
    /// <param name="city">The City of the Address.</param>
    /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if either street or city is null.</exception>
    public Address(string street, string city)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
    }

    /// <summary>
    /// Returns a new Address by invoking a MoveDelegate.
    /// </summary>
    /// <param name="mover">The delegate to invoke for the move.</param>
    /// <param name="newStreet">The new Address's Street.</param>
    /// <param name="newCity">The new Address's City.</param>
    /// <returns>A new Address object.</returns>
    public Address Move(MoveDelegate mover, string newStreet, string newCity)
    {
        var newAddress = mover(newStreet, newCity);

        return newAddress;
    }
}