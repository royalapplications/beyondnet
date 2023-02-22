namespace NativeAOTLibraryTest;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string FullName { 
        get {
            return $"{FirstName} {LastName}";
        }
    }

    public int Age { get; set; }

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