using Application.Global_Models;
using Domain;
using ISAdminWeb.Models;
using ISAdminWeb.Service;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Domain.Enums;

namespace ISAdminWeb.Controllers
{

    public class BaseController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public static string refreshedToken = "";
        public static object __refreshTokenLock = new object();
        public static object __getTokenLock = new object();
        bool __lockWasTaken = false;
        private readonly RefreshToken _refreshToken;


        public static string Base64Encode(string plainText)
        {
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }



        [AllowAnonymous]
        public async Task<UserClaim> GetUserClaims()
        {
            try
            {
                var cookie = GetLanguageCookie();

                var claimPrincipal = User as ClaimsPrincipal;

                var claimIdentity = claimPrincipal.Identity as ClaimsIdentity;

                UserClaim userClaim = new UserClaim();
                if (claimIdentity?.Claims.Count() != 0)
                {
                    var IdClaim = claimIdentity.Claims.Single(c => c.Type == "ID");
                    var FullNameClaim = claimIdentity.Claims.Single(c => c.Type == "FullName");
                    var UiLanguageClaim = claimIdentity.Claims.Single(c => c.Type == "UiLanguage");
                    var EmailClaim = claimIdentity.Claims.Single(c => c.Type == ClaimTypes.Email);
                    var PictureClaim = claimIdentity.Claims.Single(c => c.Type == "Picture");

                    userClaim.Id = int.Parse(IdClaim.Value);
                    userClaim.FullName = FullNameClaim.Value;
                    userClaim.UiLanguage = UiLanguageClaim.Value;
                    userClaim.Email = EmailClaim.Value;
                    userClaim.Picture = PictureClaim.Value;
                }
                else
                {

                    userClaim.Id = 0;
                    userClaim.FullName = string.Empty;
                    userClaim.UiLanguage = cookie;
                    userClaim.Email = string.Empty;
                    userClaim.Picture = string.Empty;
                }

                return userClaim;
            }
            catch (Exception ex)
            {
                var cookie = GetLanguageCookie();
                UserClaim userClaim = new UserClaim()
                {
                    Id = 0,
                    FullName = string.Empty,
                    UiLanguage = cookie,
                    Email = string.Empty,
                    Picture = string.Empty,
                };

                return userClaim;
            }
        }

        public string GetLanguageCookie()
        {
            return "ru";
        }

        public string GetToken()
        {
            MutexToken.Mutex.WaitOne();
            try
            {
          
                var claimPrincipal = User as ClaimsPrincipal;
                var claimIdentity = claimPrincipal.Identity as ClaimsIdentity;

                var claim = (from c in claimPrincipal.Claims
                             where c.Type == ".AspNetCore.Admin"
                             select c).FirstOrDefault();
                //преобразует строку в безопасную для использования в URI форму. 
                return Uri.EscapeDataString(claim.Value.ToString());
            }
            finally
            {
                MutexToken.Mutex.ReleaseMutex();
            }
        }


        public async Task<bool> UpdateUserClaims(Update_UserViewModel settingsView)
        {
            bool retObject = true;
            try
            {
                var claimPrincipal = User as ClaimsPrincipal;

                var claimIdentity = claimPrincipal.Identity as ClaimsIdentity;

                // Получаем полное имя из клаймов
                var claimFullName = claimPrincipal.Claims.Single(c => c.Type == ClaimTypes.Name);

                // Получаем email из клаймов
                var claimEmail = claimPrincipal.Claims.Single(c => c.Type == ClaimTypes.Email);

                // Получаем номер телефона из клаймов
                var claimPhoneNumber = claimPrincipal.Claims.Single(c => c.Type == "PhoneNumber");

                // Получаем изображение профиля из клаймов
                var claimPicture = claimPrincipal.Claims.Single(c => c.Type == "Picture");

                await HttpContext.SignOutAsync();

                claimIdentity.TryRemoveClaim(claimFullName);
                claimIdentity.TryRemoveClaim(claimEmail);
                claimIdentity.TryRemoveClaim(claimPhoneNumber);
                claimIdentity.TryRemoveClaim(claimPicture);

                var userPic = "/assets/images/no-photo.jpg";

                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, settingsView.FirstName + " " + settingsView.LastName));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, settingsView.Email));
                claimIdentity.AddClaim(new Claim("PhoneNumber", settingsView.PhoneNumber));
                claimIdentity.AddClaim(new Claim("Picture", userPic));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

                return retObject;
            }
            catch (Exception ex)
            {
                retObject = false;
                return retObject;
            }
        }
        #region Returns
        protected virtual IActionResult CreateJsonOk(string message = null, bool showToast = false)
        {
            return Json(new BaseJsonResponse { Result = ExecutionResult.OK, Message = message, ShowToast = showToast });
        }
        public class SingleJsonResponse<TRecord> : BaseJsonResponse where TRecord : class
        {
            public TRecord Record { get; set; }
        }
        protected virtual IActionResult CreateJsonOk<RecordModelT>(RecordModelT record, string message = null, bool showToast = false) where RecordModelT : class
        {
            return Json(new SingleJsonResponse<RecordModelT> { Result = ExecutionResult.OK, Message = message, ShowToast = showToast, Record = record });
        }

        protected virtual IActionResult CreateJsonKo(string message = null, bool showToast = false)
        {
            return Json(new BaseJsonResponse { Result = ExecutionResult.KO, Message = message, ShowToast = showToast });
        }


        protected virtual IActionResult CreateJsonLogout(string message = null, bool showToast = false)
        {
            return Json(new BaseJsonResponse { Result = ExecutionResult.LOGOUT, Message = message, ShowToast = showToast });
        }
        #endregion

    }
}
