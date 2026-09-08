using System.Net;

namespace Beyond.NET.Sample;

public class WebProxyTests
{
    public static WebProxy CreateWebProxy()
    {
        var proxy = new WebProxy();
        return proxy;
    }
}
