namespace NativeAOTLibraryTest;

public class Company
{
    private readonly List<Person> m_employees = new();

    public string Name { get; }

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

    public void AddEmployee(Person employee)
    {
        m_employees.Add(employee);
    }

    public void RemoveEmployee(Person employee)
    {
        m_employees.Remove(employee);
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