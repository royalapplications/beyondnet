using System;
using System.Text.Json.Serialization;
using NativeAOT.Core;

namespace NativeAOTSample;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore]
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
    
    // Uncomment for debugging
    // ~Person() {
    //     Console.WriteLine($"Did call finalizer of {nameof(Person)}.");
    // }

    public void ReduceAge(int byYears)
    {
        int newAge = Age - byYears;

        if (newAge < 0) {
            throw new Exception("Age cannot be negative.");
        }

        Age = newAge;
    }
    
    public void ChangeAge(Func<int>? newAgeProvider)
    {
        if (newAgeProvider is null) {
            return;
        }
        
        int newAge = newAgeProvider();

        if (newAge < 0) {
            throw new Exception("Age cannot be negative.");
        }

        Age = newAge;
    }

    #region Equality
    public override bool Equals(object? other)
    {
        Person? otherPerson = other as Person;

        if (otherPerson is null) {
            return false;
        }

        bool isEqual = FirstName == otherPerson.FirstName &&
                       LastName == otherPerson.LastName &&
                       Age == otherPerson.Age;

        return isEqual;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            FirstName,
            LastName,
            Age
        );
    }
    #endregion Equality
}