namespace Beyond.NET.Core;

public class Services: IServices
{
    private static Services? m_shared;
    public static Services Shared
    {
        get {
            Services? shared = m_shared;

            if (shared is null) {
                shared = new();
                m_shared = shared;
            }

            return shared;
        }
    }

    #region IServices
    public ILogger LoggerService { get; } = new ConsoleLogger();
    #endregion IServices
}