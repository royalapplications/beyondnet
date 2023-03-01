using System;
using NativeAOT.Core;

namespace NativeAOTSample;

[NativeExport]
public class Person
{
    [NativeExport]
    public string FirstName { get; set; }
    
    [NativeExport]
    public string LastName { get; set; }

    [NativeExport]
    public string FullName 
    {
        get {
            return $"{FirstName} {LastName}";
        }
    }

    [NativeExport]
    public int Age { get; set; }

    [NativeExport]
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

    [NativeExport]
    public void ReduceAge(int byYears)
    {
        int newAge = Age - byYears;

        if (newAge < 0) {
            throw new Exception("Age cannot be negative.");
        }

        Age = newAge;
    }
    
    [NativeExport]
    public void ChangeAge(Func<int>? newAgeProvider)
    {
        if (newAgeProvider == null) {
            return;
        }
        
        int newAge = newAgeProvider();

        if (newAge < 0) {
            throw new Exception("Age cannot be negative.");
        }

        Age = newAge;
    }
}