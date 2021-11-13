using System;

namespace HttpParser
{
    public static class Parser
    {
        public static ParsedHttpRequest ParseRawRequest(string raw, IgnoreHttpParserOptions options = null)
        {
            var index = raw.IndexOf("\r\n\r\n", StringComparison.Ordinal);
            string headers, body;
            if (index < 0)
            {
                headers = raw;
                body = null;
            }
            else
            {
                headers = raw.Substring(0, index);
                body = raw.Substring(index + 4);
            }

            var headerLines = SplitLines(headers);
            var requestLine = new RequestLine(headerLines);
            var requestHeaders = new RequestHeaders(headerLines);
            requestHeaders.AddHeader(RequestHeaders.Method, requestLine.Method);
            requestHeaders.AddHeader(RequestHeaders.HttpVersion, requestLine.HttpVersion);
            var requestCookies = new RequestCookies(headerLines);
            //var requestBody = new RequestBody(requestLine, lines);

            var parsed = new ParsedHttpRequest(options)
            {
                Url = requestLine.Url,
                Uri = new Uri(requestLine.Url),
                Headers = requestHeaders.Headers,
                Cookies = requestCookies.ParsedCookies,
                RequestBody = body
            };

            parsed.ApplyIgnoreOptions();

            return parsed;
        }

        private static string[] SplitLines(string raw)
        {
            return raw
                .TrimEnd('\r', '\n')
                .Split(new[] { "\\n", "\n", "\r\n" }, StringSplitOptions.None);
        }
    }
}
