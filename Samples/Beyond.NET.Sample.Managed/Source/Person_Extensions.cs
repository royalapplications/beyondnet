namespace Beyond.NET.Sample;

public static class Person_Extensions
{
    public static void IncreaseAge(
        this Person person,
        int byYears
    )
    {
        person.Age += byYears;
    }

    public static bool TryGetAddress(
        this Person person,
        out Address? address
    )
    {
        address = person.Address;

        return address is not null;
    }
}