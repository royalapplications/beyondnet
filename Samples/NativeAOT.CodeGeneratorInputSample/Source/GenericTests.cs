using System.Reflection;
using System.Reflection.Emit;

namespace NativeAOT.CodeGeneratorInputSample;

public class GenericTests
{
    private int m_test = 2;
    
    public static Type ReturnGenericType<T>()
    {
        return typeof(T);
    }
    
    public static Type[] ReturnGenericTypes<T1, T2>()
    {
        return new [] {
            typeof(T1),
            typeof(T2)
        };
    }

    public static T? ReturnDefaultValueOfGenericType<T>()
    {
        return default;
    }
    
    public static T?[] ReturnArrayOfDefaultValuesOfGenericType<T>(int numberOfElements)
    {
        if (numberOfElements <= 0) {
            return Array.Empty<T>();
        }
        
        List<T?> defaultValues = new();

        for (int i = 0; i < numberOfElements; i++) {
            defaultValues.Add(default);
        }
        
        return defaultValues.ToArray();
    }

    public T[] ReturnArrayOfRepeatedValues<T>(T value, int numberOfElements)
    {
        if (numberOfElements <= 0) {
            return Array.Empty<T>();
        }

        List<T> values = new();

        for (int i = 0; i < numberOfElements; i++) {
            values.Add(value);
        }

        return values.ToArray();
    }

    // public void GenericMethod<T>(T o) where T: class, IAnimal
    // {
    //     
    // }
    //
    // public object[] ReturnArrayOfRepeatedValues(Type T, object value, int numberOfElements)
    // {
    //     dynamic a = "abc";
    //     GenericMethod(a);
    //     
    //     var b = (Action<IAnimal>)GenericMethod<IAnimal>;
    //
    //     return null;
    // }

    public double DynamicMethodTest(int theValue)
    {
        string methodName = string.Empty;
        Type owner = typeof(GenericTests);
        Type[] args = { typeof(GenericTests), typeof(int) };
        Type returnType = typeof(int);
        
        DynamicMethod multiplyHidden = new(
            methodName,
            returnType,
            args,
            owner
        );
        
        // Emit the method body. In this example ILGenerator is used
        // to emit the MSIL. DynamicMethod has an associated type
        // DynamicILInfo that can be used in conjunction with
        // unmanaged code generators.
        //
        // The MSIL loads the first argument, which is an instance of
        // the Example class, and uses it to load the value of a
        // private instance field of type int. The second argument is
        // loaded, and the two numbers are multiplied. If the result
        // is larger than int, the value is truncated and the most
        // significant bits are discarded. The method returns, with
        // the return value on the stack.
        //
        ILGenerator generator = multiplyHidden.GetILGenerator();
        
        generator.Emit(OpCodes.Ldarg_0);

        FieldInfo testInfo = owner.GetField(
            nameof(m_test),
            BindingFlags.NonPublic | BindingFlags.Instance
        )!;

        generator.Emit(OpCodes.Ldfld, testInfo);
        generator.Emit(OpCodes.Ldarg_1);
        generator.Emit(OpCodes.Mul);
        generator.Emit(OpCodes.Ret);
        
        // Create a delegate that represents the dynamic method.
        // Creating the delegate completes the method, and any further
        // attempts to change the method — for example, by adding more
        // MSIL — are ignored.
        //
        // The following code binds the method to a new instance
        // of the Example class whose private test field is set to 42.
        // That is, each time the delegate is invoked the instance of
        // Example is passed to the first parameter of the method.
        //
        // The delegate OneParameter is used, because the first
        // parameter of the method receives the instance of Example.
        // When the delegate is invoked, only the second parameter is
        // required.
        //
        Func<int, int> invoker = (Func<int, int>)multiplyHidden.CreateDelegate(
            typeof(Func<int, int>), 
            this
        );

        int result = invoker(theValue);

        return result;
    }
    
    public static Type CallReturnGenericTypeThroughReflection(Type T)
    {
        object? methodTarget = null;
        object[]? parameters = null;
        
        Type targetType = typeof(GenericTests);
        string nameOfMethod = nameof(ReturnGenericType);
        Type[] genericParameterTypes = new[] { T };
        Type[] parameterTypes = Type.EmptyTypes;

        System.Reflection.MethodInfo? method = targetType.GetMethod(
            nameOfMethod,
            genericParameterTypes.Length,
            parameterTypes
        );

        if (method == null) {
            throw new Exception($"Method \"{nameOfMethod}\" not found");
        }

        MethodInfo genericMethod = method.MakeGenericMethod(genericParameterTypes);
        
        object? untypedReturnValue = genericMethod.Invoke(
            methodTarget,
            parameters
        );

        Type returnValue = (untypedReturnValue as Type)!;
        
        return returnValue;
    }
}