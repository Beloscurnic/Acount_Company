using Application.Global_Models;
using Application.Service;
using Application.Service.ISAuthService;
using Application.Service.Token;
using Domain;
using MediatR;
using Newtonsoft.Json;

namespace Application.Requests.Commands.UpserCompany
{
    public partial class EditCompany
    {
        public class Handler : IRequestHandler<Command, CompanyResponse>
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

            public async Task<CompanyResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    bool BigSizeImg = false;
                    var url = authURLs.UPSertCompany();
                    var credential = "";
                    var token = _tokenService.Refresh_token(request.Token, request._Delegat);
                    Company upsertViewModel = new Company()
                    {
                        ID = request.UpserCompany.ID,
                        Bank = request.UpserCompany.Bank,
                        BIC = request.UpserCompany.BIC,
                        CommercialName = request.UpserCompany.CommercialName,
                        Email = request.UpserCompany.Email,
                        IBAN = request.UpserCompany.IBAN,
                        IDNO = request.UpserCompany.IDNO,
                        IsVATPayer = request.UpserCompany.IsVATPayer,
                        JuridicalAddress = request.UpserCompany.JuridicalAddress,
                        JuridicalName = request.UpserCompany.JuridicalName,
                        OfficeAddress = request.UpserCompany.OfficeAddress,
                        PhoneNumber = request.UpserCompany.PhoneNumber,
                        PostalCode = request.UpserCompany.PostalCode,
                        Status = request.UpserCompany.Status,
                        VATCode = request.UpserCompany.VATCode,
                        WebSite = request.UpserCompany.WebSite,
                        BasePartnerID = request.UpserCompany.BasePartnerID,
                        CountryID = request.UpserCompany.CountryID,
                        CreateDate = request.UpserCompany.CreateDate,
                        ShortName = request.UpserCompany.ShortName,
                    };

                    if (request.UpserCompany.ESMAlias != null)
                    {
                        upsertViewModel.ESMAlias = String.Join(",", request.UpserCompany.ESMAlias);
                    }

                    var companyedit = new CompanyUpsertPost()
                    {
                        Company = upsertViewModel,
                        Token = request.Token,
                    };
                    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };

                    var json = JsonConvert.SerializeObject(companyedit, microsoftDateFormatSettings);
                    QueryDataPost queryDataPost = new QueryDataPost()
                    {
                        JSON = json,
                        URL = url,
                        Credentials = ""
                    };

                    var queryResponse = await _globalQuery.PostAsync(queryDataPost);

                    var jsonObj = JsonConvert.DeserializeObject<CompanyResponse>(queryResponse);

                    if (jsonObj.ErrorCode == EnErrorCode.Expired_token)
                    {
                        token = _tokenService.Refresh_token(request.Token, request._Delegat);
                        return await Handle(new Command(request.UpserCompany, request.Token, request._Delegat), cancellationToken);
                    }
                    return jsonObj;
                }
                catch (Exception ex)
                {
                    var upserModelCompany = new CompanyResponse()
                    {
                        ErrorCode = EnErrorCode.Expired_token,
                        ErrorMessage = ex.Message,
                    };
                    return upserModelCompany;
                }
            }
        }
    }
}
