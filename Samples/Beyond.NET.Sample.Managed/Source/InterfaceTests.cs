namespace Beyond.NET.Sample;

public interface IInterface1
{
    void MethodInIInterface1();
}

public interface IInterface2
{
    int PropertyInIInterface2 { get; set; }
}

public class TypeThatImplementsMultipleInterfaces: IInterface1, IInterface2
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
}

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
}