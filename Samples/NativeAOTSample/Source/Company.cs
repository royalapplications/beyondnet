using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NativeAOTSample;

public class Company
{
    private List<Person> m_employees = new();

    // This is only for JSON serialization
    [JsonPropertyName("Employees")]
    public Person[] _Employees
    {
        get {
            return m_employees.ToArray();
        }
        set {
            m_employees = value.ToList();
        }
    }

    public delegate void NumberOfEmployeesChangedDelegate();

    [JsonIgnore]
    public NumberOfEmployeesChangedDelegate? NumberOfEmployeesChanged { get; set; }

    public string Name { get; set; }

    [JsonIgnore]
    public int NumberOfEmployees
    {
        get {
            return m_employees.Count;
        }
    }

    public Company(string name)
    {
        Name = name;
    }
    
    // Uncomment for debugging
    // ~Company() {
    //     Console.WriteLine($"Did call finalizer of {nameof(Company)}.");
    // }

    public void AddEmployee(Person employee)
    {
        m_employees.Add(employee);
        
        NumberOfEmployeesChanged?.Invoke();
    }

    public void RemoveEmployee(Person employee)
    {
        m_employees.Remove(employee);
        
        NumberOfEmployeesChanged?.Invoke();
    }

    public bool ContainsEmployee(Person employee)
    {
        return m_employees.Contains(employee);
    }

    public Person? GetEmployeeAtIndex(int index)
    {
        if (index < 0 ||
            index >= m_employees.Count) {
            return null;
        }

        Person employee = m_employees[index];

        return employee;
    }
}