using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace HttpWebRequestExecutor
{
    public class ParsedWebResponse
    {
        public string ResponseText { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Cookies { get; set; }
        public Uri ResponseUri { get; set; }
        public Dictionary<string, string[]> ResponseHeaders { get; set; }

        public ParsedWebResponse() { }
        public ParsedWebResponse(HttpWebResponse response)
        {
            ResponseText = response.ResponseString();
            StatusCode = (int)response.StatusCode;
            StatusDescription = response.StatusDescription;
            ResponseHeaders = ConvertWebHeadersToDictionary(response.Headers);
            Cookies = response.Headers["Set-Cookie"];
            ResponseUri = response.ResponseUri;
        }

        private static Dictionary<string, string[]> ConvertWebHeadersToDictionary(NameValueCollection headers)
        {
            return Enumerable.Range(0, headers.Count).ToDictionary(i => headers.Keys[i], headers.GetValues);
        }
    }
}
