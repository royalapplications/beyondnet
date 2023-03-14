namespace NativeAOT.CodeGeneratorInputSample;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

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

    public string GetWelcomeMessage()
    {
        return $"Welcome, {FullName}! You're {Age} years old.";
    }
}