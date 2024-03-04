
using Application.Requests.Queries.GetCompany.GetCompanyInfo;
using Application.Requests.Queries.GetCompany.List;
using Application.Service.Token;
using Domain;
using ISAdminWeb.Models;
using ISAdminWeb.Models.Company;
using ISAdminWeb.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISAdminWeb.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class CompanyController : BaseController
    {

        public static string refreshedToken = "";
        public static object __refreshTokenLock = new object();
        public static object __getTokenLock = new object();
        bool __lockWasTaken = false;
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        private readonly RefreshToken _refreshToken;

        public CompanyController(IMediator mediator, ITokenService tokenService, RefreshToken refreshToken)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _refreshToken = refreshToken;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            return View("~/Views/Company/Index.cshtml");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Companies()
        {
            return PartialView("~/Views/Company/_CompanieList.cshtml");
        }

        [HttpGet("Get_Company_List")]
        public async Task<IActionResult> Get_Company_List()
        {
            try
            {
                string token = GetToken();
                var basequery = new BaseQueryModel()
                {
                    Token = token,
                    _Delegat = (t) => _refreshToken.RefreshTokenClaim(t)
                };

                var query = new Get_Company_List.Query(basequery.Token, basequery._Delegat);

                var response = await _mediator.Send(query);

                if (response.ErrorCode == EnErrorCode.Expired_token)
                {
                    return CreateJsonLogout();
                }
                else if (response.ErrorCode == EnErrorCode.Invalid_token)
                {
                    return CreateJsonLogout();
                }

                return Json(response.CompanyList.OrderByDescending(x => x.ID));
            }
            catch (Exception ex)
            {
                var jsonRespons = JsonConvert.SerializeObject(ex);
                return new JsonResult(jsonRespons);
            }
        }

        [HttpGet("GetCompanyInfo/{ID}")]
        public async Task<IActionResult> GetCompanyInfo(int ID)
        {
            var companyUpsertViewModel = new UPSertCompany_ViewModel();
            try
            {
                string token = GetToken();
                var basequery = new BaseQueryModel()
                {
                    Token = token,
                    _Delegat = (t) => _refreshToken.RefreshTokenClaim(t)
                };

                var query = new GetCompanyInfo.Query(basequery.Token, basequery._Delegat, ID);

                var response = await _mediator.Send(query);

                if (response.ErrorCode == 143)
                {
                    return CreateJsonLogout();
                }
                else if (response.ErrorCode == 143)
                {
                    return CreateJsonLogout();
                }

                companyUpsertViewModel = new UPSertCompany_ViewModel()
                {
                    ID = response.Company.ID,
                    Bank = response.Company.Bank,
                    BIC = response.Company.BIC,
                    CommercialName = response.Company.CommercialName,
                    Email = response.Company.Email,
                    IBAN = response.Company.IBAN,
                    IDNO = response.Company.IDNO,
                    IsVATPayer = response.Company.IsVATPayer,
                    JuridicalAddress = response.Company.JuridicalAddress,
                    JuridicalName = response.Company.JuridicalName,
                    OfficeAddress = response.Company.OfficeAddress,
                    PhoneNumber = response.Company.PhoneNumber,
                    PostalCode = response.Company.PostalCode,
                    Status = response.Company.Status,
                    VATCode = response.Company.VATCode,
                    WebSite = response.Company.WebSite,
                    ShortName = response.Company.ShortName,
                };

                return PartialView("~/Views/Company/_ChangeStatusCompany.cshtml", companyUpsertViewModel);
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Company/_ChangeStatusCompany.cshtml", companyUpsertViewModel);
            }
        }

        [HttpGet("CompanyID/{ID}")]
        public async Task<IActionResult> CompanyID(int ID)
        {
            try
            {
                var companyUpsertViewModel = new UPSertCompany_ViewModel();

                if (ID == 0)
                {
                    companyUpsertViewModel.ID = ID;
                    return PartialView("~/Views/Company/_UpsertCompany.cshtml", companyUpsertViewModel);
                }
                else
                {
                    string token = GetToken();
                    var basequery = new BaseQueryModel()
                    {
                        Token = token,
                        _Delegat = (t) => _refreshToken.RefreshTokenClaim(t)
                    };

                    var query = new Application.Requests.Queries.GetCompany.CompanyGet.GetCompany_ID.Query(basequery.Token, basequery._Delegat, ID);

                    var response = await _mediator.Send(query);

                    companyUpsertViewModel = new UPSertCompany_ViewModel()
                    {
                        ID = response.Company.ID,
                        Bank = response.Company.Bank,
                        BIC = response.Company.BIC,
                        CommercialName = response.Company.CommercialName,
                        Email = response.Company.Email,
                        IBAN = response.Company.IBAN,
                        IDNO = response.Company.IDNO,
                        IsVATPayer = response.Company.IsVATPayer,
                        JuridicalAddress = response.Company.JuridicalAddress,
                        JuridicalName = response.Company.JuridicalName,
                        OfficeAddress = response.Company.OfficeAddress,
                        PhoneNumber = response.Company.PhoneNumber,
                        PostalCode = response.Company.PostalCode,
                        Status = response.Company.Status,
                        VATCode = response.Company.VATCode,
                        WebSite = response.Company.WebSite,
                        ShortName = response.Company.ShortName,
                    };

                    if (response.Company.ESMAlias != null)
                    {
                        companyUpsertViewModel.ESMAlias = response.Company.ESMAlias.Split(",");
                    }

                    if (response.Company.CountryID != null)
                    {
                        companyUpsertViewModel.CountryID = (int)response.Company.CountryID;
                    }

                    if (response.ErrorCode == 143)
                    {
                        return CreateJsonLogout();
                    }
                    return PartialView("~/Views/Company/_UpsertCompany.cshtml", companyUpsertViewModel);
                }
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Company/_UpsertCompany.cshtml");
            }
        }

        [HttpPost("UpsertCompany")]
        public async Task<IActionResult> UpsertCompany(UPSertCompany_ViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("~/Views/Company/_UpsertCompany.cshtml", viewModel);
                }

                string token = GetToken();
                var basequery = new BaseQueryModel()
                {
                    Token = token,
                    _Delegat = (t) => _refreshToken.RefreshTokenClaim(t)
                };

                var editmodel = new Application.Requests.Commands.UpserCompany.EditCompany.View_Model
                {
                    ID = viewModel.ID,
                    CommercialName = viewModel.CommercialName,
                    Email = viewModel.Email,
                    IDNO = viewModel.IDNO,
                    JuridicalName = viewModel.JuridicalName,
                    Picture = viewModel.Picture,
                    Status = viewModel.Status,
                    BIC = viewModel.BIC,
                    Bank = viewModel.Bank,
                    BasePartnerID = viewModel.BasePartnerID,
                    CountryID = viewModel.CountryID,
                    CreateDate = viewModel.CreateDate,
                    IBAN = viewModel.IBAN,
                    IsVATPayer = viewModel.IsVATPayer,
                    JuridicalAddress = viewModel.JuridicalAddress,
                    OfficeAddress = viewModel.OfficeAddress,
                    PhoneNumber = viewModel.PhoneNumber,
                    PostalCode = viewModel.PostalCode,
                    ShortName = viewModel.ShortName,
                    ESMAlias = viewModel.ESMAlias,
                    VATCode = viewModel.VATCode,
                    WebSite = viewModel.WebSite
                };

                var command = new Application.Requests.Commands.UpserCompany.EditCompany.Command(editmodel, basequery.Token, basequery._Delegat);

                var response = await _mediator.Send(command);


                if (response.ErrorCode == EnErrorCode.Expired_token)
                {
                    return await UpsertCompany(viewModel);
                }

                return Json("OK");
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Company/_UpsertCompany.cshtml", viewModel);
            }
        }

        [HttpGet("Status_Company")]
        public IActionResult Status_Company()
        {
            var dictionary = new Dictionary<string, int>()
            {
                {"NewRegistered",0},
                {"Activated",1},
                {"Disabled",2},
            };
            return Json(dictionary.OrderBy(x => x.Value));
        }

        [HttpGet("ChangeStatusCompanyGet")]
        public async Task<IActionResult> ChangeStatusCompanyGet([FromQuery] int ID, [FromQuery] string status)
        {
            ViewBag.StatusCompany = status;
            try
            {

                var companyUpsertViewModel = new UPSertCompany_ViewModel();
                string token = GetToken();
                var basequery = new BaseQueryModel()
                {
                    Token = token,
                    _Delegat = (t) => _refreshToken.RefreshTokenClaim(t)
                };

                var query = new Application.Requests.Queries.GetCompany.CompanyGet.GetCompany_ID.Query(basequery.Token, basequery._Delegat, ID);

                var response = await _mediator.Send(query);

                companyUpsertViewModel = new UPSertCompany_ViewModel()
                {
                    ID = response.Company.ID,
                    Bank = response.Company.Bank,
                    BIC = response.Company.BIC,
                    CommercialName = response.Company.CommercialName,
                    Email = response.Company.Email,
                    IBAN = response.Company.IBAN,
                    IDNO = response.Company.IDNO,
                    IsVATPayer = response.Company.IsVATPayer,
                    JuridicalAddress = response.Company.JuridicalAddress,
                    JuridicalName = response.Company.JuridicalName,
                    OfficeAddress = response.Company.OfficeAddress,
                    PhoneNumber = response.Company.PhoneNumber,
                    PostalCode = response.Company.PostalCode,
                    Status = response.Company.Status,
                    VATCode = response.Company.VATCode,
                    WebSite = response.Company.WebSite,
                    ShortName = response.Company.ShortName,
                };

                if (response.Company.ESMAlias != null)
                {
                    companyUpsertViewModel.ESMAlias = response.Company.ESMAlias.Split(",");
                }

                if (response.Company.CountryID != null)
                {
                    companyUpsertViewModel.CountryID = (int)response.Company.CountryID;
                }

                if (response.ErrorCode == 143)
                {
                    return CreateJsonLogout();
                }
                return PartialView("~/Views/Company/_ChangeStatusCompany.cshtml", companyUpsertViewModel);

            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Company/_ChangeStatusCompany.cshtml");
            }
        }

        [HttpGet("ChangeStatusCompany")]
        public async Task<IActionResult> ChangeStatusCompany([FromQuery] int id, [FromQuery] string status)
        {
            ViewBag.HeaderText = status;
            try
            {
                string token = GetToken();
                var basequery = new BaseQueryModel()
                {
                    Token = token,
                    _Delegat = (t) => _refreshToken.RefreshTokenClaim(t)
                };

                var query = new Application.Requests.Commands.ChangeCompanyStatus.ChangeCompanyStatus.Command(id, status, basequery.Token, basequery._Delegat);

                var response = await _mediator.Send(query);

                if (response.ErrorCode == EnErrorCode.Expired_token)
                {
                    return CreateJsonLogout();
                }
                else if (response.ErrorCode == EnErrorCode.Invalid_token)
                {
                    return CreateJsonLogout();
                }
                // return PartialView("~/Views/Company/_ChangeStatusCompany.cshtml", response.);
                return Json("OK");
            }
            catch (Exception ex)
            {
                var jsonRespons = JsonConvert.SerializeObject(ex);
                return new JsonResult(jsonRespons);
            }
        }
    }
}
