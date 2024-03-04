using Application.Global_Models;
using Application.Service;
using Application.Service.ISAuthService;
using Application.Service.Token;
using Domain;
using MediatR;
using Newtonsoft.Json;

namespace Application.Requests.Queries.GetCompany.List
{
    public partial class Get_Company_List
    {
        public class Handler : IRequestHandler<Query, View_Model_List>
        {
            private readonly IAuthURLs authURLs;
            private readonly GlobalQuery _globalQuery;
            private readonly ITokenService _tokenService;

            public Handler(IAuthURLs _authURLs, GlobalQuery globalQuery, ITokenService tokenService)
            {
                authURLs = _authURLs;
                _globalQuery = globalQuery;
                _tokenService = tokenService;
            }

            public async Task<View_Model_List> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var url = authURLs.GetCompanyList(request.Token);
                    var credentials = authURLs.Credentials();

                    QueryDataGet queryDataGet = new QueryDataGet()
                    {
                        URL = url,
                        Credentials = credentials
                    };

                    var queryResponse = await _globalQuery.GetAsync(queryDataGet);
                    var jsonObj = JsonConvert.DeserializeObject<View_Model_List>(queryResponse);

                    if (jsonObj.ErrorCode == EnErrorCode.Expired_token)
                    {
                        var token = _tokenService.Refresh_token(request.Token, request._Delegat);
                        return await Handle(new Query(token.Token, request._Delegat), cancellationToken);
                    }
                    return jsonObj;
                }
                catch (Exception ex)
                {
                    View_Model_List baseResponse = new View_Model_List()
                    {
                        ErrorCode = EnErrorCode.Internal_error,
                        ErrorMessage = ex.Message,
                    };

                    return baseResponse;
                }
            }
        }
    }
}
