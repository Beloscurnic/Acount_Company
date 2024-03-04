using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain
{
    public class Enums
    {
        public enum ExecutionResult
        {
            OK = 1,
            KO = 2,
            ERROR = 3,
            NOTVALID = 4,
            EXCEPTION = 5,
            LOGOUT = 6,
        }
        public enum EUserStatus
        {
            [Display(Name = "Новая регистрация")]
            NewRegistered = 0,
            [Display(Name = "Пользователь активен")]
            Active = 1,
            [Display(Name = "Деактивировано")]
            Disabled = 2,
        }

        public enum EnUiLanguage
        {
            EN = 0,
            RO = 1,
            RU = 2,

        }

        public enum ECompanyStatus
        {
            [Display(Name = "NewRegistered")]
            NewRegistered = 0,
            [Display(Name = "Activated")]
            Active = 1,
            [Display(Name = "Disabled")]
            Disabled = 2,

        }
    }
}
