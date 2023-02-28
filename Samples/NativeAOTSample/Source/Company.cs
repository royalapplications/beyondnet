namespace NativeAOTSample;

public class Company
{
    private readonly List<Person> m_employees = new();
    
    public delegate void NumberOfEmployeesChangedDelegate();

    public NumberOfEmployeesChangedDelegate? NumberOfEmployeesChanged { get; set; }

    public string Name { get; set; }

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