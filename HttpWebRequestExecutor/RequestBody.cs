using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpParser
{
    internal class RequestBody
    {
        public RequestBody(RequestLine requestLine, IReadOnlyList<string> lines)
        {
            switch (requestLine.Method)
            {
                case RequestLine.GET:
                    Body = SetBodyFromUrlGet(requestLine.Url);
                    break;
                case RequestLine.POST:
                    Body = SetBodyFromPost(lines);
                    break;
                default: throw new Exception(nameof(RequestBody));
            }
        }

        public string Body { get; set; }

        private static string SetBodyFromUrlGet(string url)
        {
            return url.Contains('?') ? url.Split('?')[1] : null;
        }

        private static string SetBodyFromPost(IReadOnlyList<string> lines)
        {
            var index = lines.Count;
            return index == -1 ? null : lines[index - 1];
        }
    }
}