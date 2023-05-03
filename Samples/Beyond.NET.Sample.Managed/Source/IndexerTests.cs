namespace Beyond.NET.Sample;

public class IndexerTests
{
    public string StoredString { get; private set; } = string.Empty;
    public int StoredNumber { get; private set; } = 0;
    public Guid StoredGuid { get; private set; } = Guid.Empty;
    
    public object[] StoredValue { get; private set; } = new object[] {
        string.Empty,
        0, 
        Guid.Empty
    };
    
    public object[] this[string aString, int aNumber, Guid aGuid]
    {
        get {
            return new object[] { aString, aNumber, aGuid };
        }
        set {
            StoredString = aString;
            StoredNumber = aNumber;
            StoredGuid = aGuid;
            
            StoredValue = value;
        }
    }
}