namespace Beyond.NET.Sample;

// This is declared by the user.
public interface IInterface1
{
    void MethodInIInterface1();
}

// This should be auto-generated to allow Swift to provide implementations for .NET interfaces.
public class IInterface1_DelegateAdapter : IInterface1
{
    public delegate void MethodInIInterface1_Delegate();
    private MethodInIInterface1_Delegate _MethodInIInterface1_Adapter;

    public IInterface1_DelegateAdapter(MethodInIInterface1_Delegate methodInIInterface1_Adapter)
    {
        _MethodInIInterface1_Adapter = methodInIInterface1_Adapter;
    }
    
    public void MethodInIInterface1()
    {
        _MethodInIInterface1_Adapter();
    }
}

public interface IInterface2
{
    int PropertyInIInterface2 { get; set; }
}

public interface IInterface3 : IInterface1, IInterface2
{
    void MethodInIInterface3();
}

public class TypeThatImplementsMultipleInterfaces: IInterface1, IInterface2, IInterface3
{
    public void MethodInIInterface1()
    {
        Console.WriteLine($"{nameof(MethodInIInterface1)} called through {nameof(TypeThatImplementsMultipleInterfaces)}");
    }

    private int _propertyInIInterface2; 
    public int PropertyInIInterface2
    {
        get
        {
            Console.WriteLine($"Getter of {nameof(PropertyInIInterface2)} called through {nameof(TypeThatImplementsMultipleInterfaces)}");

            return _propertyInIInterface2;
        }
        set
        {
            Console.WriteLine($"Setter of {nameof(PropertyInIInterface2)} called through {nameof(TypeThatImplementsMultipleInterfaces)}");

            _propertyInIInterface2 = value;
        }
    }

    public void MethodInIInterface3()
    {
        Console.WriteLine($"{nameof(MethodInIInterface3)} called through {nameof(TypeThatImplementsMultipleInterfaces)}");
    }
}

// This is declared by the user.
public class TypeThatUsesInterfaces
{
    public void CallMethod1InIInterface1(IInterface1 interface1)
    {
        interface1.MethodInIInterface1();
    }
    
    public void SetPropertyInIInterface2(IInterface2 interface2, int value)
    {
        interface2.PropertyInIInterface2 = value;
    }
    
    public int GetPropertyInIInterface2(IInterface2 interface2)
    {
        return interface2.PropertyInIInterface2;
    }
    
    public void CallMethod1InIInterface3(IInterface3 interface3)
    {
        interface3.MethodInIInterface3();
    }
}