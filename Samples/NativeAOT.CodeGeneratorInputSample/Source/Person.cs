namespace NativeAOT.CodeGeneratorInputSample;

public enum NiceLevels: uint
{
    NotNice,
    LittleBitNice,
    Nice,
    VeryNice
}

public class Person
{
    public const int AGE_WHEN_BORN = 0;
    public static int DEFAULT_AGE = AGE_WHEN_BORN;
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Person[] Children { get; private set; } = Array.Empty<Person>();

    public Array ChildrenAsArray
    {
        get {
            return Children;
        }
    }

    public NiceLevels NiceLevel { get; set; } = NiceLevels.Nice;

    public string FullName => $"{FirstName} {LastName}";
    
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
}