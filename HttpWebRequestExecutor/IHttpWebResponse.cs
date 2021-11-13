using System;
using System.IO;

namespace HttpWebRequestExecutor
{
    public interface IHttpWebResponse : IDisposable
    {
        Stream GetResponseStream();
        ParsedWebResponse GetParsedWebResponse();
    }
}
