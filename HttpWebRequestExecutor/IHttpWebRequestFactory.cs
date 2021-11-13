namespace HttpParser
{
    public interface IHttpWebRequestFactory
    {
        IHttpWebRequest BuildRequest(ParsedHttpRequest parsed);
    }
}
