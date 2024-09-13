using System.Diagnostics.CodeAnalysis;

namespace Beyond.NET.Sample;

public class Person
{
    #region Constants
    public const int AGE_WHEN_BORN = 0;
    public const int DEFAULT_AGE = AGE_WHEN_BORN;
    #endregion Constants

    #region Properties
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Address? Address { get; set; }
    public Uri? Website { get; set; }

    private Person[] m_children = Array.Empty<Person>();

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
    
    public NiceLevels NiceLevel { get; set; } = NiceLevels.Nice;

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