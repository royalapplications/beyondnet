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
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

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

    public string GetFirstName()
    {
        return FirstName;
    }

    public void SetFirstName(string firstName)
    {
        FirstName = firstName;
    }
    
    public string GetLastName()
    {
        return LastName;
    }

    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    public int GetAge()
    {
        return Age;
    }

    public void SetAge(int age)
    {
        Age = age;
    }

    public string GetFullName()
    {
        return FullName;
    }

    public NiceLevels GetNiceLevel()
    {
        return NiceLevel;
    }

    public void SetNiceLevel(NiceLevels niceLevel)
    {
        NiceLevel = niceLevel;
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
}