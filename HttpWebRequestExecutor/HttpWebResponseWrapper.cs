using System;
using System.IO;
using System.Net;

namespace HttpParser
{
    internal class HttpWebResponseWrapper : IHttpWebResponse
    {
        private HttpWebResponse _response;

        public HttpWebResponseWrapper(HttpWebResponse response)
        {
            this._response = response;
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }

        public ParsedWebResponse GetParsedWebResponse()
        {
            return new ParsedWebResponse(_response);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || _response == null) return;

            _response.Dispose();
            _response = null;
        }
    }
}
