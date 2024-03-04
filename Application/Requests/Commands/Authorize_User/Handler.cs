using Application.Global_Models;
using Application.Service;
using Application.Service.ISAuthService;
using Domain;
using MediatR;
using Newtonsoft.Json;

namespace Application.Requests.Commands.Authorize_User
{
    public partial class Authorize_User
    {
        public class Handler : IRequestHandler<Command, AuthResponse>
        {
            private readonly IAuthURLs authURLs;
            private readonly GlobalQuery GlobalQuery;
            public Handler(IAuthURLs _authURLs, GlobalQuery _GlobalQuery)
            {
                authURLs = _authURLs;
                GlobalQuery = _GlobalQuery;
            }

            public async Task<AuthResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var url = authURLs.AuthorizeUser();
                    var credentials = "";
                    var json = JsonConvert.SerializeObject(request);

                    QueryDataPost queryDataPost = new QueryDataPost()
                    {
                        JSON = json,
                        URL = url,
                        Credentials = credentials,
                    };

                    var queryResponse = await GlobalQuery.PostAsync(queryDataPost);

                    return JsonConvert.DeserializeObject<AuthResponse>(queryResponse);
                }
                catch (Exception ex)
                {
                    AuthResponse authResponse = new AuthResponse()
                    {
                        ErrorCode = EnErrorCode.Internal_error,
                        ErrorMessage = ex.Message
                    };
                    return authResponse;
                }
            }
        }
    }
}
