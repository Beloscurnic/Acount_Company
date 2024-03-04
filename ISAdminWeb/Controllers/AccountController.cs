using Application.Global_Models;
using Domain;
using ISAdminWeb.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Domain.Enums;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace ISAdminWeb.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        

        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
 
                ViewBag.Language = "ru";


            return View("~/Views/Account/_Login.cshtml");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthorizeViewModel_DTO authorizeViewModel)
        {
            try
            {
                ViewBag.Language = "ru";

                if (ModelState.IsValid)
                {
                    authorizeViewModel.Email = authorizeViewModel?.Email?.ToLower().Trim();

                    var command = new Application.Requests.Commands.Authorize_User.Authorize_User.Command(authorizeViewModel.Email, authorizeViewModel.Password);
                    var response = await _mediator.Send(command);

                    if (response.ErrorCode == EnErrorCode.NoError)
                    {
                        var uiLanguage = Enums.EnUiLanguage.RU;

                        List<Claim> userClaims = new List<Claim>();
                        if (response.User != null)
                        {

                            userClaims.Add(new Claim("IsAdministrator", "true"));
                            userClaims.Add(new Claim("Navigations", "Allow"));

                            uiLanguage = EnUiLanguage.RU;
                            userClaims.Add(new Claim("ID", response.User.ID.ToString()));
                            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, response.User.ID.ToString()));
                            userClaims.Add(new Claim(ClaimTypes.Email, response.User.Email));
                            userClaims.Add(new Claim(ClaimTypes.Name, response.User.FirstName + " " + response.User.LastName));
                            userClaims.Add(new Claim("FullName", response.User.FirstName + " " + response.User.LastName));
                            userClaims.Add(new Claim("Company", response.User.Company));
                            userClaims.Add(new Claim("PhoneNumber", response.User.PhoneNumber));
                            userClaims.Add(new Claim("UiLanguage", uiLanguage.ToString()));
                            userClaims.Add(new Claim("Picture", "/assets/images/no-photo.jpg"));
                            userClaims.Add(new Claim(".AspNetCore.Admin", response.Token));

                            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(uiLanguage.ToString())), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                        }

                        var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var claimsPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else if (response.ErrorCode == EnErrorCode.User_name_not_found_or_incorrect_password)
                    {
                        ModelState.AddModelError("Email", "Пользователь не найден или введен неверный пароль");
                        return View("~/Views/Account/_Login.cshtml", authorizeViewModel);
                    }
                    else if (response.ErrorCode == EnErrorCode.Internal_error)
                    {
                        ModelState.AddModelError("Email", "Внутреняя ошибка");
                        return View("~/Views/Account/_Login.cshtml", authorizeViewModel);
                    }
                    else if (response.ErrorCode != EnErrorCode.NoError)
                    {
                        ModelState.AddModelError("Password", response.ErrorMessage + ". " + "Свяжитесь с администратором");

                        return View("~/Views/Account/_Login.cshtml", authorizeViewModel);
                    }
                }

                return View("~/Views/Account/_Login.cshtml", authorizeViewModel);
            }
            catch (Exception ex)
            {
                BaseResponse errorResponse = new BaseResponse()
                {
                    ErrorCode = EnErrorCode.Internal_error,
                    ErrorMessage = ex.Message,
                };
                return PartialView("~/Views/Home/_500.cshtml", errorResponse);
            }
        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View("~/Views/Account/_ForgotPassword.cshtml");
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel_DTO forgotPasswordViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = new Application.Requests.Commands.ForgotPassword.ForgotPas.Command(forgotPasswordViewModel.Email, forgotPasswordViewModel.INFO);
                    var response = await _mediator.Send(command);

                    if (response.ErrorCode == EnErrorCode.User_name_not_found_or_incorrect_Email)
                    {
                        ModelState.AddModelError("Email", "Пользователь не найден или введен неверный пароль");
                        return PartialView("~/Views/Account/_ForgotPassword.cshtml", forgotPasswordViewModel);
                    }
                    else if (response.ErrorCode == EnErrorCode.Internal_error)
                    {
                        ModelState.AddModelError("Email", "Внутреняя ошибка");
                        return PartialView("~/Views/Account/_ForgotPassword.cshtml", forgotPasswordViewModel);
                    }
                    else if (response.ErrorCode != EnErrorCode.NoError)
                    {
                        ModelState.AddModelError("Email", response.ErrorMessage + ". " + "Свяжитесь с администратором");
                        return PartialView("~/Views/Account/_ForgotPassword.cshtml", forgotPasswordViewModel);
                    }

                    return PartialView("~/Views/Account/_Login.cshtml", forgotPasswordViewModel);
                }
                else
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }

                return View("~/Views/Account/_ForgotPassword.cshtml", forgotPasswordViewModel);
            }
            catch (Exception ex)
            {
                BaseResponse errorResponse = new BaseResponse()
                {
                    ErrorCode = EnErrorCode.Internal_error,
                    ErrorMessage = ex.Message,
                };

                return PartialView("~/Views/Home/_500.cshtml", errorResponse);
            }
        }

        [HttpGet("TokenLogout")]
        public async Task<IActionResult> TokenLogout()
        {
            //TempData["InvalidToken"] = Localization.SessionTimeout;

            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

    }
}
