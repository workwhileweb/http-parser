﻿using HttpParser;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RequestLineTests
    {
        [Test]
        public void Should_Parse_Request_Line()
        {
            var line = new[] {"GET https://www.example.com HTTP/1.1"};

            var requestLine = new RequestLine(line);

            Assert.AreEqual("GET", requestLine.Method);
            Assert.AreEqual("https://www.example.com", requestLine.Url);
            Assert.AreEqual("HTTP/1.1", requestLine.HttpVersion);
        }

        [Test]
        public void Should_Throw_For_Bad_Method()
        {
            var line = new[] {"PUT https://www.example.com HTTP/1.1"};

            var ex = Assert.Throws<CouldNotParseHttpRequestException>(() => new RequestLine(line));
            Assert.AreEqual("Not a valid HTTP Verb Method: SetHttpMethod() Data: PUT", ex.Message);
        }

        [Test]
        public void Should_Throw_For_Bad_Url()
        {
            var line = new[] {"GET www.example.com HTTP/1.1"};

            var ex = Assert.Throws<CouldNotParseHttpRequestException>(() => new RequestLine(line));
            Assert.AreEqual("URL is not in a valid format Method: SetUrl() Data: www.example.com", ex.Message);
        }
    }
}