using System;
using System.IO;

namespace HttpParser
{
    public interface IHttpWebResponse : IDisposable
    {
        Stream GetResponseStream();
        ParsedWebResponse GetParsedWebResponse();
    }
}
