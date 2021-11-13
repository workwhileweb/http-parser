using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HttpParser
{
    public class RequestLine
    {
        private const string ProtocolHttp = "HTTP/";
        private const string ProtocolHttpRegEx = ProtocolHttp + @"\d.\d";
        private const string ProtocolHttp11 = ProtocolHttp + "1.1";

        // ReSharper disable once InconsistentNaming
        public const string POST = nameof(POST);
        // ReSharper disable once InconsistentNaming
        public const string GET = nameof(GET);

        public string Method { get; set; }
        public string Url { get; set; }
        public string HttpVersion { get; set; }

        private readonly string[] _validHttpVerbs = { GET, POST };

        public RequestLine(IReadOnlyList<string> lines)
        {
            var firstLine = lines[0].Split(' ');
            ValidateRequestLine(firstLine);
            SetHttpMethod(firstLine[0]);
            SetUrl(firstLine[1]);
            SetHttpVersion(firstLine[2]);
        }

        private static void ValidateRequestLine(IReadOnlyCollection<string> firstLine)
        {
            if (firstLine.Count != 3) throw new Exception(nameof(ValidateRequestLine));
        }

        private void SetHttpMethod(string method)
        {
            method = method.Trim().ToUpper();
            if (!_validHttpVerbs.Contains(method)) throw new Exception(method);
            Method = method;
        }

        private void SetUrl(string url)
        {
            if (!IsValidUri(url, out _)) throw new Exception(url);
            Url = url.Trim();
        }

        private static bool IsValidUri(string url, out Uri uriResult)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private void SetHttpVersion(string version)
        {
            HttpVersion = !Regex.IsMatch(version, ProtocolHttpRegEx) ? ProtocolHttp11 : version.Trim();
        }
    }
}