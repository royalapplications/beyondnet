using System.Diagnostics.CodeAnalysis;

namespace Beyond.NET.Sample;

public class Person
{
    #region Constants
    /// <summary>
    /// A Person's Age when Born
    /// </summary>
    public const int AGE_WHEN_BORN = 0;

    /// <summary>
    /// The default Person's Age.
    /// </summary>
    public const int DEFAULT_AGE = AGE_WHEN_BORN;
    #endregion Constants

    #region Properties
    /// <summary>
    /// The Person's First Name
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The Person's Last Name
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// The Person's Age
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// The Person's Postal Address
    /// </summary>
    public Address? Address { get; set; }

    /// <summary>
    /// The Person's Website
    /// </summary>
    public Uri? Website { get; set; }

    private Person[] m_children = Array.Empty<Person>();

    /// <summary>
    /// The Person's Children
    /// </summary>
    public Person[] Children
    {
        get {
            return m_children;
        }
        set {
            m_children = value;

            NumberOfChildrenChanged?.Invoke();
        }
    }

    public event NumberOfChildrenChangedDelegate? NumberOfChildrenChanged;

    /// <summary>
    /// Answers the question: How nice is this Person?
    /// </summary>
    public NiceLevels NiceLevel { get; set; } = NiceLevels.Nice;

    /// <summary>
    /// The Person's Full Name
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
    #endregion Properties

    #region Delegate Types
    public delegate void NumberOfChildrenChangedDelegate();
    public delegate int NewAgeProviderDelegate();
    #endregion Delegate Types

    #region Constructors
    public Person(
        string firstName,
        string lastName,
        int age
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public Person(
        string firstName,
        string lastName
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Age = DEFAULT_AGE;
    }
    #endregion Constructors

    #region Methods
    public static Person MakeJohnDoe() => new("John", "Doe", 50);

    public string GetNiceLevelString()
    {
        switch (NiceLevel) {
            case NiceLevels.NotNice:
                return "Not nice";
            case NiceLevels.LittleBitNice:
                return "A little bit nice";
            case NiceLevels.Nice:
                return "Nice";
            case NiceLevels.VeryNice:
                return "Very nice";
        }

        throw new Exception("Unknown nice level");
    }

    public string GetWelcomeMessage()
    {
        return $"Welcome, {FullName}! You're {Age} years old and {GetNiceLevelString()}.";
    }

    public void AddChild(Person child)
    {
        if (Children.Contains(child)) {
            return;
        }

        List<Person> children = Children.ToList();

        children.Add(child);

        Children = children.ToArray();
    }

    public void RemoveChild(Person child)
    {
        if (!Children.Contains(child)) {
            return;
        }

        List<Person> children = Children.ToList();

        children.Remove(child);

        Children = children.ToArray();
    }

    public void RemoveChildAt(int index)
    {
        List<Person> children = Children.ToList();

        children.RemoveAt(index);

        Children = children.ToArray();
    }

    public int NumberOfChildren
    {
        get {
            return Children.Length;
        }
    }

    public Person ChildAt(int index)
    {
        return Children[index];
    }

    public bool TryGetChildAt(int index, [MaybeNullWhen(false)] out Person child)
    {
        if (index < 0 || index >= Children.Length) {
            child = null;

            return false;
        }

        child = Children[index];

        return true;
    }

    public void ChangeAge(NewAgeProviderDelegate? newAgeProvider)
    {
        if (newAgeProvider is null) {
            return;
        }

        var newAge = newAgeProvider();

        Age = newAge;
    }
    #endregion Methods
}
