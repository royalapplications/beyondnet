namespace NativeAOTSample;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string FullName 
    {
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

    public void ReduceAge(int byYears)
    {
        int newAge = Age - byYears;

        if (newAge < 0) {
            throw new Exception("Age cannot be negative.");
        }

        Age = newAge;
    }
}