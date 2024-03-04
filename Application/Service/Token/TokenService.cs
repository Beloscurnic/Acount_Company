using Application.Global_Models;
using Application.Service.ISAuthService;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Application.Service.Token
{
    public class TokenService : ITokenService
    {
        private readonly IAuthURLs _authURLs;
        private readonly GlobalQuery _globalQuery;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public static object __refreshTokenLock2 = new object();
        public TokenService(IAuthURLs authURLs, GlobalQuery globalQuery)
        {
            _authURLs = authURLs;
            _globalQuery = globalQuery;
        }

        public Model_Token_Response Refresh_token(string requestToken, Func<string, bool> delegat)
        {
            Monitor.Enter(__refreshTokenLock2);
            try
            {
                var urltoken = _authURLs.RefreshToken(requestToken);
                var credentialstoken = _authURLs.Credentials();

                QueryDataGet queryDataGettoken = new QueryDataGet()
                {
                    URL = urltoken,
                    Credentials = credentialstoken
                };

                var queryResponsetoken = _globalQuery.Get(queryDataGettoken);

                var token = JsonConvert.DeserializeObject<Model_Token_Response>(queryResponsetoken);

                if (token.ErrorCode == EnErrorCode.Expired_token)
                {
                    Refresh_token(requestToken, delegat);
                }

                else if (token.ErrorCode == EnErrorCode.Internal_error)
                {
                    throw new Exception(token.ErrorMessage);
                }
                bool result = delegat(token.Token);
                return token;
            }
            finally
            {
                Monitor.Exit(__refreshTokenLock2);
            }
        }
    }
}
