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

    [NativeExport]
    public void ReduceAge(int byYears)
    {
        int newAge = Age - byYears;

        if (newAge < 0) {
            throw new Exception("Age cannot be negative.");
        }

        Age = newAge;
    }
}