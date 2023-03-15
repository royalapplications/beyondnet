namespace NativeAOT.CodeGeneratorInputSample;

public static class Person_Extensions
{
    public static void IncreaseAge(
        this Person person, 
        int byYears
    )
    {
        person.Age += byYears;
    }
}