namespace Beyond.NET.Sample.Source;

public interface IOverrideTestsInterface
{
    
}

public class OverrideTestsInterfaceImpl: IOverrideTestsInterface
{
    
}

public class OverrideTestsBaseClass
{
    public virtual IOverrideTestsInterface? ReturnInterfaceOrImpl()
    {
        return null;
    }
    
    public virtual IOverrideTestsInterface? ReturnInterface()
    {
        return null;
    }

    public virtual int GetInt()
    {
        return 0;
    }
}

public class OverrideTestsDerivedClass: OverrideTestsBaseClass
{
    public new OverrideTestsInterfaceImpl? ReturnInterfaceOrImpl()
    {
        return null;
    }

    public override IOverrideTestsInterface? ReturnInterface()
    {
        return null;
    }

    public override int GetInt()
    {
        return 1;
    }
}