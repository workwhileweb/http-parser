using System.Net;

namespace HttpWebRequestExecutor
{
    public static class HttpWebResponseExtensions
    {
        public static string ResponseString(this HttpWebResponse resp)
        {
            using (var stream = resp.GetResponseStream())
            {
                return resp.Headers["Content-Encoding"] == "gzip" 
                    ? stream.DecompressGzipStream().GetStringFromStream() 
                    : stream.GetStringFromStream();
            }
        }
    }
}