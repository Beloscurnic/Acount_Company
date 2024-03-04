namespace Application.Service.Token
{
    public interface ITokenService
    {
      public Model_Token_Response Refresh_token(string requestToken,  Func<string, bool> delegat);
    }
}
