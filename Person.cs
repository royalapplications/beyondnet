namespace NativeAOTLibraryTest;

public class Person
{
    public string FirstName { get; }
    public string LastName { get; }

    public string FullName { 
        get {
            return $"{FirstName} {LastName}";
        }
    }

    public int Age { get; }

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
}