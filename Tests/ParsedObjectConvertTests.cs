using HttpParser;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ParsedObjectConvertTests
    {
        [TestCase(FakeRawRequests.GetWithoutQueryString)]
        [TestCase(FakeRawRequests.PostWithRequestBody)]
        public void Should_Convert_ParsedRequest_Back_To_String(string input)
        {
            var parsed = Parser.ParseRawRequest(input);

            Assert.AreEqual(input, parsed.ToString());
        }

        [TestCase(FakeRawRequests.PostWithRequestBody)]
        public void Should_Strip_Cookies(string input)
        {
            var parsed = Parser.ParseRawRequest(input, new IgnoreHttpParserOptions {IgnoreCookies = true});
            ;

            Assert.AreEqual(RequestCookiesStripped, parsed.ToString());
        }

        private const string RequestCookiesStripped = @"POST https://httpbin.org/post HTTP/1.1
Host: httpbin.org
User-Agent: curl/7.54.1
Accept: */*
Content-Type: application/x-www-form-urlencoded

helloworld";
    }
}