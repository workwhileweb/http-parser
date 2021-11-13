using System;
using System.Net;
using HttpParser;

namespace Run
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                IHttpWebRequestFactory factory = new HttpWebRequestFactory();
                var rr = new RequestRunner(factory);
                rr.Run();
            }
            catch(WebException wex)
            {
                Console.WriteLine(wex.Message);
            }
        }
    }
}