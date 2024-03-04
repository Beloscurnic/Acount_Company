using Application.Global_Models;
using Application.Requests.Commands.Authorize_User;
using Application.Service;
using Application.Service.ISAuthService;
using Domain;
using MediatR;
using Newtonsoft.Json;

namespace Application.Requests.Commands.ForgotPassword
{
    public partial class ForgotPas
    {
        public class Handler : IRequestHandler<Command, BaseResponse>
        {
            private readonly IAuthURLs authURLs;
            private readonly GlobalQuery GlobalQuery;
            public Handler(IAuthURLs _authURLs, GlobalQuery _GlobalQuery)
            {
                authURLs = _authURLs;
                GlobalQuery = _GlobalQuery;
            }
            public async Task<BaseResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var url = authURLs.AuthorizeUser();
                    var credential = "";
                    var json = JsonConvert.SerializeObject(request);

                    QueryDataPost queryDataPost = new QueryDataPost()
                    {
                        JSON = json,
                        URL = url,
                        Credentials = ""
                    };

                    var queryResponse = await GlobalQuery.PostAsync(queryDataPost);
                    return JsonConvert.DeserializeObject<BaseResponse>(queryResponse);
                }
                catch (Exception ex)
                {
                    BaseResponse baseResponse = new BaseResponse()
                    {
                        ErrorCode = EnErrorCode.Internal_error,
                        ErrorMessage = ex.Message
                    };
                    return baseResponse;
                }
            }
        }
    }
}
