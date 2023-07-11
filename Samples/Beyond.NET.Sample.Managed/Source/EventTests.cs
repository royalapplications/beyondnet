namespace Beyond.NET.Sample;

public class EventTests
{
    public delegate void ValueChangedDelegate(object sender, int newValue);

    public event ValueChangedDelegate? ValueChanged;

    private int m_value;
    public int Value
    {
        get => m_value;
        set {
            m_value = value;
            ValueChanged?.Invoke(this, value);
        }
    }
}