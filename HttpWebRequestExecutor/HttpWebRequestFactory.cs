namespace HttpParser
{
    public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequest BuildRequest(ParsedHttpRequest parsed)
        {
            var request = HttpWebRequestBuilder.InitializeWebRequest(parsed);
            return new HttpWebRequestWrapper(request);
        }
    }
}