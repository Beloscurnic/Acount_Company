using Application.Global_Models;
using Application.Service.ISAuthService;
using Application.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;
using Application.Service.Token;
using static Domain.Enums;
using System.Net;

namespace Application.Requests.Commands.ChangeCompanyStatus
{
    public partial class ChangeCompanyStatus {
        public class Handler : IRequestHandler<Command, BaseResponse>
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

            public async Task<BaseResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var idstatus = 0;
                    if(request.Status == "Disabled")
                    {
                        idstatus = 1;
                    }
                    else if (request.Status == "Activated")
                    {
                        idstatus = 2;
                    }
                    var url = authURLs.ChangeCompanyStatus(request.Token, request.ID, idstatus);

                    QueryDataGet queryDataGet = new QueryDataGet()
                    {
                        URL = url,
                        Credentials ="",
                    };

                    var queryResponse = await _globalQuery.GetAsync(queryDataGet);

                    var jsonObj = JsonConvert.DeserializeObject<BaseResponse>(queryResponse);

                    if (jsonObj.ErrorCode == EnErrorCode.Expired_token)
                    {
                        var token = _tokenService.Refresh_token(request.Token, request._Delegat);
                        return await Handle(new Command(request.ID, request.Status, request.Token, request._Delegat), cancellationToken);
                    }
                    return jsonObj;
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
