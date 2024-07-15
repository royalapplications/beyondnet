namespace Beyond.NET.Sample;

// This is declared by the user.
public interface IInterface1
{
    void MethodInIInterface1();
}

// This should be auto-generated to allow Swift to provide implementations for .NET interfaces.
// NOTE: Right now, every struct gets an auto-generated parameterless constructor. For these special interface delegate adapters we should omit that to prevent misuse.
public readonly struct IInterface1_DelegateAdapter : IInterface1
{
    public delegate void MethodInIInterface1_Delegate();
    private readonly MethodInIInterface1_Delegate _MethodInIInterface1_Adapter;

    public IInterface1_DelegateAdapter(MethodInIInterface1_Delegate methodInIInterface1_Adapter)
    {
        _MethodInIInterface1_Adapter = methodInIInterface1_Adapter ?? throw new ArgumentNullException(nameof(methodInIInterface1_Adapter));
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
    public delegate void DelegateThatReceivesIInterface1(IInterface1 interface1);
    public delegate IInterface1 DelegateThatReturnsIInterface1();
    
    public void CallMethod1InIInterface1(IInterface1 interface1)
    {
        interface1.MethodInIInterface1();
    }
    
    public void DelegateThatReceivesInterfaceTest(DelegateThatReceivesIInterface1 del, IInterface1 interface1)
    {
        del(interface1);
    }
    
    public IInterface1 DelegateThatReturnsInterfaceTest(DelegateThatReturnsIInterface1 del)
    {
        return del();
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

    public IInterface1 GetTypeThatImplementsIInterface1()
    {
        return new TypeThatImplementsMultipleInterfaces();
    }
    
    public void GetTypeThatImplementsIInterface1AsOutParam(out IInterface1 interface1)
    {
        interface1 = new TypeThatImplementsMultipleInterfaces();
    }
    
    public IInterface2 GetTypeThatImplementsIInterface2()
    {
        return new TypeThatImplementsMultipleInterfaces();
    }
    
    public void GetTypeThatMaybeImplementsIInterface2AsOutParam(out IInterface2? interface2)
    {
        interface2 = null;
    }
    
    public IInterface3 GetTypeThatImplementsIInterface3()
    {
        return new TypeThatImplementsMultipleInterfaces();
    }
}

public interface IRegistrationData
{
    DataType DataType { get; }
    static abstract DataType RegisteredDataType { get; }
    static abstract void RegisterDataType();
}

public abstract class BaseRegistrationData
{
    public DataType DataType { get; }

    protected BaseRegistrationData(DataType dataType)
    {
        DataType = dataType;
    }
}

public sealed class RegistrationData1 : BaseRegistrationData, IRegistrationData  
{
    public static DataType RegisteredDataType { get; } = default;

    public static void RegisterDataType() {}

    public RegistrationData1()
        : base(RegisteredDataType)
    { }
}

public sealed class RegistrationData2 : BaseRegistrationData, IRegistrationData  
{
    public static DataType RegisteredDataType { get; } = default;

    public static void RegisterDataType() {}

    public RegistrationData2()
        : base(RegisteredDataType)
    { }
}

public readonly struct DataType;

public interface INotImplementedInterfaceWithStaticAbstractMembers
{
    static abstract DataType RegisteredDataType { get; }
    static abstract void RegisterDataType();
}