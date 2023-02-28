using System;
using System.Collections.Generic;

using NativeAOT.Core;

namespace NativeAOTSample;

public class Test { }

[NativeExport]
public class Company
{
    private readonly List<Person> m_employees = new();
    
    public delegate void NumberOfEmployeesChangedDelegate();

    [NativeExport]
    public NumberOfEmployeesChangedDelegate? NumberOfEmployeesChanged { get; set; }

    [NativeExport]
    public string Name { get; set; }

    [NativeExport]
    public int NumberOfEmployees
    {
        get {
            return m_employees.Count;
        }
    }

    [NativeExport]
    public Company(string name)
    {
        Name = name;
    }

    [NativeExport]
    public void AddEmployee(Person employee)
    {
        m_employees.Add(employee);
        
        NumberOfEmployeesChanged?.Invoke();
    }

    [NativeExport]
    public void RemoveEmployee(Person employee)
    {
        m_employees.Remove(employee);
        
        NumberOfEmployeesChanged?.Invoke();
    }

    [NativeExport]
    public bool ContainsEmployee(Person employee)
    {
        return m_employees.Contains(employee);
    }

    [NativeExport]
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