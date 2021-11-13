﻿using System;
using HttpParser;

namespace Run
{
    public class RequestRunner
    {
        public const string GetWithoutQueryString = @"GET https://httpbin.org/get HTTP/1.1
Host: httpbin.org
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36
Upgrade-Insecure-Requests: 1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9";

        private readonly IHttpWebRequestFactory _factory;

        public RequestRunner(IHttpWebRequestFactory factory)
        {
            _factory = factory;
        }

        public void Run()
        {
            var parsed = Parser.ParseRawRequest(GetWithoutQueryString);
            var req = _factory.BuildRequest(parsed);

            var resp = req.GetResponse();
            Console.WriteLine(resp.GetParsedWebResponse().ResponseText);
        }
    }
}