﻿using System.Net;

namespace HttpParser
{
    internal class HttpWebRequestWrapper : IHttpWebRequest
    {
        private readonly HttpWebRequest _request;

        public HttpWebRequestWrapper(HttpWebRequest request)
        {
            this._request = request;
        }

        public IHttpWebResponse GetResponse()
        {
            return new HttpWebResponseWrapper((HttpWebResponse)_request.GetResponse());
        }
    }
}
