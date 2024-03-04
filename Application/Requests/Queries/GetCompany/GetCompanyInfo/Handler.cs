using Application.Global_Models;
using Application.Service.ISAuthService;
using Application.Service.Token;
using Application.Service;
using Domain;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Requests.Queries.GetCompany.List.Get_Company_List;

namespace Application.Requests.Queries.GetCompany.GetCompanyInfo
{
    public partial class GetCompanyInfo
    {
        public class Handler : IRequestHandler<Query, View_Model>
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

            public async Task<View_Model> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var url = authURLs.GetCompanyInfo(request.Token, request.Id);
                    var credentials = authURLs.Credentials();

                    QueryDataGet queryDataGet = new QueryDataGet()
                    {
                        URL = url,
                        Credentials = credentials
                    };

                    var queryResponse = await _globalQuery.GetAsync(queryDataGet);
                    var jsonObj = JsonConvert.DeserializeObject<View_Model>(queryResponse);

                    if (jsonObj.ErrorCode == 143)
                    {
                        var token = _tokenService.Refresh_token(request.Token, request._Delegat );
                        return await Handle(new Query(token.Token, request._Delegat, request.Id), cancellationToken);
                    }
                  
                    return jsonObj;
                }
                catch (Exception ex)
                {
                    View_Model baseResponse = new View_Model()
                    {
                        ErrorCode = 143,
                        ErrorMessage = ex.Message,
                    };

                    return baseResponse;
                }
            }
        }
    }
}