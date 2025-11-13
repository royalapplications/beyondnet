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

    extension(Person person)
    {
        /// <summary>
        /// Creates a Person named "Jane Doe" who is 50 years old.
        /// Implemented as a .NET 10 static extension method.
        /// </summary>
        /// <returns>Your new Jane Doe</returns>
        public static Person MakeJaneDoe() => new("Jane", "Doe", 50);


        /// <summary>
        /// Increases the target person's age by 1.
        /// Implemented as a .NET 10 extension method.
        /// </summary>
        public void CelerbrateBirthday()
        {
            person.IncreaseAge(1);
        }

        /// <summary>
        /// The getter returns `true` if the target person's nice level is `NotNice` or "lower". When `true` is passed to the setter, it sets the person's nice level to `NotNice`, when `false` it doesn't do anything.
        /// Implemented as a .NET 10 extension property.
        /// </summary>
        public bool IsMean
        {
            get => person.NiceLevel <= NiceLevels.NotNice;
            set {
                if (value) {
                    person.NiceLevel = NiceLevels.NotNice;
                }
            }
        }
    }
}
