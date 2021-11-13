namespace HttpWebRequestExecutor
{
    public interface IHttpWebRequestFactory
    {
        IHttpWebRequest BuildRequest(ParsedHttpRequest parsed);
    }
}
