using System;

namespace HttpParser
{
    public class CouldNotParseHttpRequestException : Exception
    {
        public CouldNotParseHttpRequestException(string message, string step, string component)
            : base($"{message} Method: {step}() Data: {component}")
        {
        }
    }
}