using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HttpParser
{
    public class ParsedHttpRequest
    {
        private readonly IgnoreHttpParserOptions _ignoreHttpParserOptions;

        public ParsedHttpRequest(IgnoreHttpParserOptions options = null)
        {
            _ignoreHttpParserOptions = options;
        }

        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Cookies { get; set; }
        public string RequestBody { get; set; }
        public Uri Uri { get; set; }
        public CookieContainer CookieContainer { get; set; }

        public void ApplyIgnoreOptions()
        {
            if (_ignoreHttpParserOptions == null) return;

            if (_ignoreHttpParserOptions.IgnoreUrl) Url = null;
            if (_ignoreHttpParserOptions.IgnoreHeaders) Headers = null;
            if (_ignoreHttpParserOptions.IgnoreCookies)
            {
                Cookies = null;
                CookieContainer = null;
            }

            if (_ignoreHttpParserOptions.IgnoreRequestBody) RequestBody = null;
        }

        public override string ToString()
        {
            var method = Headers["Method"];
            var version = Headers["HttpVersion"];

            var sb = new StringBuilder($"{method} {Url} {version}{Environment.NewLine}");

            var headersToIgnore = new List<string> {"Method", "HttpVersion"};
            if (Cookies == null) headersToIgnore.Add("Cookie");

            foreach (var header in Headers.Where(header => !headersToIgnore.Contains(header.Key)))
            {
                sb.Append($"{header.Key}: {header.Value}{Environment.NewLine}");
            }

            if (Cookies?.Count > 0)
            {
                var cookies = string.Join(";", Cookies
                        .Select(cookie => $" {cookie.Key}={cookie.Value};"))
                    .TrimEnd(';');

                sb.Append($"Cookie:{cookies}{Environment.NewLine}");
            }

            if (method != "POST") return sb.ToString().Trim();
            sb.Append(Environment.NewLine);
            sb.Append(RequestBody);
            return sb.ToString().Trim();
        }
    }
}